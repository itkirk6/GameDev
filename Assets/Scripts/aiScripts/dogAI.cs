using System;
using System.Collections.Generic;
using UnityEngine;

public class dogAI : MonoBehaviour
{

    [Serializable] public class patrolPoint
    {
        public Transform waypoint;
        public float waitTime = 1f;
        public Vector2 facingAfterArrival = Vector2.down;
    }

    
    [SerializeField] private List<patrolPoint> patrolPoints = new List<patrolPoint>();
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private bool loop = true;

    [SerializeField] private AudioClip attackClip;
    private AudioSource audioSource;


    public Transform player;
    public float detectionRadius = 5f;
    public float chaseSpeed = 3.5f;
    public float attackRange = 0.2f;
    public float attackRate = 1.5f;
    public int damage = 50;

    private Animator animator;
    private Rigidbody2D rb;

    // runtime variables
    private Vector2 movementInput;
    private Vector2 lastMove = Vector2.down;

    private int currentIndex = 0;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private bool isChasing = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckForPlayer();

        if(isChasing)
        {
            isWaiting = false;
            if(Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }

        else
            handleWaitTimer();
        handleAnimations();
    }

    private void FixedUpdate()
    {
        if(isChasing)
        {
            if(Vector2.Distance(transform.position, player.position) > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                movementInput = Vector2.zero;
            }
        }
        else
            handleMovement();
    }

    private void CheckForPlayer()
    {
        if(player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
                player = playerObj.transform;
            else
                return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isChasing = (distanceToPlayer <= detectionRadius);
    }

    private void ChasePlayer()
    {
        Vector2 toTarget = (Vector2)player.position - rb.position;
        movementInput = toTarget.normalized;

        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            lastMove = new Vector2(Mathf.Sign(movementInput.x), 0f);
        else
            lastMove = new Vector2(0f, Mathf.Sign(movementInput.y));

        rb.MovePosition(rb.position + (movementInput * chaseSpeed * Time.fixedDeltaTime));
    }

    private void AttackPlayer()
    {
        if (attackClip != null)
        {
            audioSource.PlayOneShot(attackClip);
        }

        movementInput = Vector2.zero;

        Vector2 toPlayer = (player.position - transform.position).normalized;
        if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            lastMove = new Vector2(Mathf.Sign(toPlayer.x), 0f);
        else
            lastMove = new Vector2(0f, Mathf.Sign(toPlayer.y));

        if(Time.time >= nextAttackTime)
        {
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                playerInventory.takeDamage(damage);
            }
            nextAttackTime = Time.time + (1f / attackRate);
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