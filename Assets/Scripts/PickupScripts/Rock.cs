using UnityEngine;

public class Rock : MonoBehaviour, IUsableItem
{
    public GameObject rockPrefab;
    public float throwDist = 3f;
    public float throwSpeed = 8f;

    public void UseItem()
    {  
        playerController player = FindFirstObjectByType<playerController>();

        if (player == null)
        {  
            Debug.Log("player is null");
            return;
        }

        if (rockPrefab == null)
        {
            Debug.Log("rockPrefab is null");
            return;
        }

        Vector2 throwDirection = player.getLastMove();

        if (throwDirection == Vector2.zero)
        {
            throwDirection = Vector2.down;
        }

        Vector3 spawnPosition = player.transform.position + (Vector3)(throwDirection * 1f);
        GameObject thrownRock = Object.Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        rockProjectile projectile = thrownRock.GetComponent<rockProjectile>();

        if (projectile != null)
        {
            projectile.setDirection(throwDirection);
            projectile.throwSpeed = throwSpeed;
            projectile.maxDistance = throwDist;
        }
    }

}
