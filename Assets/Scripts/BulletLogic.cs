using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 3f;
    public int damage = 25;

    public void Shoot(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Guard")) return;
        if(collision.CompareTag("Player") && !collision.isTrigger) return;

        Debug.Log($"Collided with {collision.name}");
        if(collision.CompareTag("Player"))
        {
            PlayerInventory inv = collision.GetComponent<PlayerInventory>();
            if(inv.playerHealth <= damage)
                Destroy(collision.gameObject);
            else
                inv.playerHealth -= damage; 
        }      
        Destroy(gameObject);
    }
}
