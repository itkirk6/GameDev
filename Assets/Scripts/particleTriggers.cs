using UnityEngine;

public class surfaceTrigger : MonoBehaviour
{
    public bool isGrass;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        walkParticles playerParticles = other.GetComponentInChildren<walkParticles>();

        if (playerParticles != null)
        {
            playerParticles.setSurface(isGrass);
        }
    }
}
