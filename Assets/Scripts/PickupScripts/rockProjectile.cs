using UnityEngine;

public class rockProjectile : MonoBehaviour
{
    public float throwSpeed = 8f;
    public float maxDistance = 3f;

    private Vector2 moveDirection;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position += (Vector3)(moveDirection * throwSpeed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void setDirection(Vector2 newDirection)
    {
        moveDirection = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("rock hit: " + other.name);

        idleNpcController npc = other.GetComponentInParent<idleNpcController>();

        if (npc != null)
        {
            npc.knockOver();
            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
