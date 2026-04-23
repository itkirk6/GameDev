using UnityEngine;

public class showerController : MonoBehaviour
{
    public ParticleSystem waterParticles;
    public bool togShower = true;

    private bool isOn = false;

    private void Start()
    {
        if (waterParticles != null)
            waterParticles.Stop();
            waterParticles.Clear();
    }

    public void Interact()
    {
        if (waterParticles == null)
            return;

        if (togShower)
        {  
            if (isOn)
                turnOff();  
            else
                turnOn();
        }
        else
        {
            waterParticles.Play();
        }
    }

    private void turnOn()
    {
        isOn = true;
        waterParticles.Play();
    }

    private void turnOff()
    {
        isOn = false;
        waterParticles.Stop();
    }
}