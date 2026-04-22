using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string spawnPointName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<playerController>() != null)
        {
            triggerManager.instance.spawnPointName = spawnPointName;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
