using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 3f;

    public void Shoot(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            Debug.Log($"Collided with {collision.name}");
            Destroy(gameObject);
        }
    }
}
