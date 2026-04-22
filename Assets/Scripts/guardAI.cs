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

    private Animator animator;
    private Rigidbody2D rb;

    // runtime variables
    private Vector2 movementInput;
    private Vector2 lastMove = Vector2.down;

    private int currentIndex = 0;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        handleWaitTimer();
        handleAnimations();
    }

    private void FixedUpdate()
    {
        handleMovement();
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