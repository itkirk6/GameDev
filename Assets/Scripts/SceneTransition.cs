using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<playerController>() != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
