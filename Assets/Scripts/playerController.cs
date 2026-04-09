using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;


    private Animator animator;
    private Rigidbody2D rb;

    // runtime variables
    private Vector2 movementInput;
    private Vector2 lastMove = Vector2.down;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        getInput();
        handleAnimations();
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

}