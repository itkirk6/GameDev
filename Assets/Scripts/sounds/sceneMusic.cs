using UnityEngine;

public class sceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    private void Start()
    {
        if (audioController.instance != null)
        {
            audioController.instance.playMusic(musicClip);
        }
    }
}