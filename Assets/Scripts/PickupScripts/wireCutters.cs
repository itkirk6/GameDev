using UnityEngine;

public class WireCutter : MonoBehaviour, IUsableItem
{
    public float useDistance = 1.2f;
    public LayerMask Walls;

    public void UseItem()
    {
        playerController player = FindFirstObjectByType<playerController>();

        if (player == null)
            return;

        Vector2 useDirection = player.getLastMove();

        if (useDirection == Vector2.zero)
        {
            useDirection = Vector2.down;
        }

        Vector2 origin = player.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, useDirection, useDistance, Walls);

        Debug.DrawRay(origin, useDirection * useDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
            Debug.Log("Fence cut down");
        }
        else
        {
            Debug.Log("No fence in range");
        }
    }
}

