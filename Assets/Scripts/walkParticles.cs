using UnityEngine;

public class walkParticles : MonoBehaviour
{
    public ParticleSystem grassParticles;
    public ParticleSystem concreteParticles;
    public playerController playerController;
    public float moveThreshold = 0.05f;
    public Vector3 footOffset = new Vector3(0f, -0.1f, 0f);
    public float behindDistance = 0.05f;

    private Vector2 lastMoveDirection = Vector2.down;
    private ParticleSystem currentParticles;

    private void Start()
    {
        currentParticles = concreteParticles;
    }

    private void Update()
{
    //Debug.Log("walkParticles update");

    if (playerController == null)
    {
        //Debug.Log("playerController is null");
        return;
    }

    Vector2 movementInput = playerController.getMovementInput();
    //Debug.Log("movement input: " + movementInput);

    if (movementInput.magnitude > moveThreshold)
    {
        //Debug.Log("movement passed threshold");

        lastMoveDirection = playerController.getLastMove().normalized;
        updateParticlePosition();
        playCurrentParticles();
    }
    else
    {
        stopAllParticles();
    }
}

    public void setSurface(bool isOnGrass)
    {
        currentParticles = isOnGrass ? grassParticles : concreteParticles;
    }

    private void updateParticlePosition()
    {
        Vector3 behindOffset = (Vector3)(-lastMoveDirection * behindDistance);
        transform.localPosition = footOffset + behindOffset;
    }

    private void playCurrentParticles()
{

    if (currentParticles == null)
    {
        //Debug.Log("currentParticles is null");
        return;
    }


    if (!currentParticles.isPlaying)
    {
        currentParticles.Play();
    }

    if (grassParticles != currentParticles && grassParticles != null && grassParticles.isPlaying)
        grassParticles.Stop();

    if (concreteParticles != currentParticles && concreteParticles != null && concreteParticles.isPlaying)
        concreteParticles.Stop();

}

    private void stopAllParticles()
    {
        if (grassParticles != null && grassParticles.isPlaying)
            grassParticles.Stop();

        if (concreteParticles != null && concreteParticles.isPlaying)
            concreteParticles.Stop();
        //Debug.Log("stopping particles");

    }
}
