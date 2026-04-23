using UnityEngine;

public class gameController : MonoBehaviour
{
    public static gameController instance;  

    [SerializeField] private GameObject playerPrefab;  
    [SerializeField] private Transform mainSpawn;  

    private void Awake()
    {   
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }  
 
    private void Start()
    {
        spawnMain(); 
    } 

    void spawnMain()    //call to spawn in prison scene
    {
        if (playerPrefab == null || mainSpawn == null)
        {
            return;
        }

        // prevent duplicate player
        if (GameObject.FindGameObjectWithTag("Player") != null)
            return;

        Instantiate(playerPrefab, mainSpawn.position, mainSpawn.rotation);
    }
}