using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerManager : MonoBehaviour
{
    public static triggerManager instance;

    public string spawnPointName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Debug.Log("teleporting player into new scene " + spawnPointName);
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (spawnPoint != null)
        {
            Debug.Log("teleporting player at spawnpoint: " + spawnPoint.transform.position);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
        else
        {
            Debug.Log("spawnpoint is null");
        }
    }
}