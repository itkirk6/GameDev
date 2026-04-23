using UnityEngine;
using UnityEngine.Audio;
using UnityEngineInternal;

public class playerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;

    // footsteps
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private float footstepInterval = 0.4f;

    private Animator animator;
    private Rigidbody2D rb;
    private static playerController instance;

    // camera stuffs
    private Camera cam;
    [SerializeField] private Vector2 deadZone = new Vector2(1.5f, 1f);

    // runtime variables
    private AudioSource audioSource;
    private float footstepTimer;
    private Vector2 movementInput;
    private Vector2 lastMove = Vector2.down;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = FindFirstObjectByType<Camera>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        getInput();
        handleAnimations();
        handleCamera();
        handleFootsteps();
    }

    private void FixedUpdate()
    {
        handleMovement();
    }


    private void getInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        if (movementInput != Vector2.zero)
        {
            lastMove = movementInput;

            if (Mathf.Abs(movementInput.x) > 0.01f)
            {
                lastMove.x = Mathf.Sign(movementInput.x);
                lastMove.y = 0f;
            }
            else if (movementInput.y > 0)
            {
                lastMove.x = 0f;
                lastMove.y = 1f;
            }
            else if (movementInput.y < 0)
            {
                lastMove.x = 0f;
                lastMove.y = -1f;
            }
        }
    }

    private void handleMovement()
    {
        // apply movement
        rb.MovePosition(rb.position + (movementInput * movementSpeed * Time.fixedDeltaTime));
    }

    private void handleAnimations()
    {
        bool isMoving = (movementInput != Vector2.zero);
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", lastMove.x);
        animator.SetFloat("moveY", lastMove.y);
    }

    void handleCamera()
    {
        if (cam == null)
        {
            cam = FindFirstObjectByType<Camera>();
        }
        
        Vector3 camPos = cam.transform.position;

        float xDiff = transform.position.x - camPos.x;
        float yDiff = transform.position.y - camPos.y;

        if (xDiff > deadZone.x)
        {
            camPos.x = transform.position.x - deadZone.x;
        }

        if (xDiff < -deadZone.x)
        {
            camPos.x = transform.position.x + deadZone.x;
        }

        if (yDiff > deadZone.y)
        {
            camPos.y = transform.position.y - deadZone.y;
        }
        if (yDiff < -deadZone.y)
        {
            camPos.y = transform.position.y + deadZone.y;
        }

        cam.transform.position = new Vector3(camPos.x, camPos.y, cam.transform.position.z);

    }

    private void handleFootsteps()
    {
        if (movementInput != Vector2.zero)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepClip);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }



    public Vector2 getMovementInput()
    {
        return movementInput;
    }

    public Vector2 getLastMove()
    {
        return lastMove;
    }

}