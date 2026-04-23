using UnityEngine;

public class WireCutter : MonoBehaviour, IUsableItem
{
    public float useDistance = 1.2f;
    public LayerMask Walls;
    public string breakableFenceName = "breakFence";

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
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, useDirection, useDistance, Walls);

        Debug.DrawRay(origin, useDirection * useDistance, Color.red, 1f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
                continue;

            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.name == breakableFenceName)
            {
                Destroy(hitObject);
                Debug.Log("Fence cut down");
                return;
            }
        }

        Debug.Log("No breakable fence in range");
    }
}

