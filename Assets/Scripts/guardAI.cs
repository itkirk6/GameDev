using System;
using System.Collections.Generic;
using UnityEngine;

public class guardAI : MonoBehaviour
{

    [Serializable] public class patrolPoint
    {
        public Transform waypoint;
        public float waitTime = 1f;
        public Vector2 facingAfterArrival = Vector2.down;
    }

    [Header("Patrol")]
    [SerializeField] private List<patrolPoint> patrolPoints = new List<patrolPoint>();
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private bool loop = true;

    [Header("Combat")]
    public Transform player;
    public float viewDistance = 14f;
    public LayerMask LOSMask;
    public GameObject bulletPrefab;
    public float fireRate = 1.5f;

    private Animator animator;
    private Rigidbody2D rb;

    // runtime variables
    private Vector2 movementInput;
    private Vector2 lastMove = Vector2.down;

    private int currentIndex = 0;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private bool eyesOnPlayer = false;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        CheckLOS();
        Debug.Log($"Eyes on Player: {eyesOnPlayer}");
        if(eyesOnPlayer)
        {
            EngagePlayer();
        }
        else
            handleWaitTimer();

        handleAnimations();
    }

    private void FixedUpdate()
    {
        handleMovement();
    }

    private void CheckLOS()
    {
        if(player == null) return;

        eyesOnPlayer = false;

        Vector2 toPlayer = player.position - transform.position;

        if(toPlayer.magnitude <= viewDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayer.normalized, viewDistance, LOSMask);

            // --- VISUAL DEBUGGING CODE ---
            if (hit.collider != null)
            {
                // Draws a RED line to the exact point it hit something
                Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.Log($"Raycast hit a collider named: {hit.collider.name} with Tag: {hit.collider.tag}");
            }
            else
            {
                // Draws a GREEN line showing where it looked if it hit absolutely nothing
                Debug.DrawRay(transform.position, toPlayer.normalized * viewDistance, Color.green);
                Debug.Log("Raycast fired but hit absolutely nothing.");
            }
            // -----------------------------

            if(hit.collider != null && hit.collider.CompareTag("Player"))
            {
                eyesOnPlayer = true;
                return;
            }
        }
    }

    private void EngagePlayer()
    {
        movementInput = Vector2.zero;
        isWaiting = false;

        Vector2 toPlayer = (player.position - transform.position).normalized;

        if(Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            lastMove = new Vector2(Mathf.Sign(toPlayer.x), 0f);
        else
            lastMove = new Vector2(0f, Mathf.Sign(toPlayer.y));

        if(Time.time >= nextFireTime)
        {
            Shoot(toPlayer);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);

        BulletLogic bulletScript = bullet.GetComponent<BulletLogic>();
        if (bulletScript != null)
        {
            bulletScript.Shoot(direction);
        }
    }

    private void handleMovement()
    {
        if (patrolPoints.Count == 0 || isWaiting)
        {
            movementInput = Vector2.zero;
            return;
        }

        Transform target = patrolPoints[currentIndex].waypoint;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 toTarget = targetPos - currentPos;

        // arrived
        if (toTarget.magnitude <= 0.05)
        {
            rb.MovePosition(targetPos);
            startWaiting();
            return;
        }

        movementInput = toTarget.normalized;

        // snap to 4 directions
        // copied most of this code from player script
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            lastMove = new Vector2(Mathf.Sign(movementInput.x), 0f);
        }
        else
        {
            lastMove = new Vector2(0f, Mathf.Sign(movementInput.y));
        }

        rb.MovePosition(rb.position + (movementInput * movementSpeed * Time.fixedDeltaTime));
    }

    private void startWaiting()
    {
        isWaiting = true;
        movementInput = Vector2.zero;

        // set facing direction immediately
        Vector2 dir = patrolPoints[currentIndex].facingAfterArrival;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            lastMove = new Vector2(Mathf.Sign(dir.x), 0f);
        }
        else
        {
            lastMove = new Vector2(0f, Mathf.Sign(dir.y));
        }

        waitTimer = patrolPoints[currentIndex].waitTime;
    }

    private void handleWaitTimer()
    {
        if (!isWaiting)
            return;

        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0f)
        {
            advancePoint();
            isWaiting = false;
        }
    }

    private void advancePoint()
    {
        currentIndex++;

        if (currentIndex >= patrolPoints.Count)
        {
            if (loop)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = patrolPoints.Count - 1;
            }
        }
    }

    private void handleAnimations()
    {
        bool isMoving = (movementInput != Vector2.zero);

        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", lastMove.x);
        animator.SetFloat("moveY", lastMove.y);
    }
}