using UnityEngine;

public class idleNpcController : MonoBehaviour
{
    public string npcName;
    public bool canBeKnockedOver = false;
    public GameObject droppedKeyPrefab;
    public Transform dropPoint;
    public Sprite knockedOverSprite;

    private bool isKnockedOver = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void knockOver()
    {
        if (!canBeKnockedOver || isKnockedOver)
            return;

        isKnockedOver = true;

        if (knockedOverSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = knockedOverSprite;
        }

        Vector3 spawnPosition = transform.position;

        if (dropPoint != null)
        {
            spawnPosition = dropPoint.position;
        }

        if (droppedKeyPrefab != null)
        {
            Instantiate(droppedKeyPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
        }
    }
}
