using UnityEngine;
public class Gun : MonoBehaviour, IUsableItem
{
    public GameObject bulletPrefab;
    public Transform firedFrom;

    void Start()
    {
        firedFrom = FindFirstObjectByType<playerController>().transform;
    }

    public void UseItem()
    {
        if(bulletPrefab != null)
            Instantiate(bulletPrefab, firedFrom.position, firedFrom.rotation);
        else
            Debug.Log("No prefab assigned to bulletPrefab");
        Debug.Log("Gun was used");
    }   
}