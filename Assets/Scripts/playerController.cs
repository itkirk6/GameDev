using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;



    private Rigidbody2D rb;

    // runtime variables
    private Vector2 movementInput;
    private 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        getInput();
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
    }

    private void handleMovement()
    {
        // apply movement
        rb.MovePosition(rb.position + (movementInput * movementSpeed * Time.fixedDeltaTime));
    }

}
