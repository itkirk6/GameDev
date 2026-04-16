using UnityEngine;
public class Gun : MonoBehaviour, IUsable
{
    public GameObject bulletPrefab;
    public Transform firedFrom;

    public void UseItem()
    {
        if(bulletPrefab != null)
            Instantiate(bulletPrefab, firedFrom.position, firedFrom.rotation);
        else
            Debug.Log("No prefab assigned to bulletPrefab");
        Debug.Log("Gun was used");
    }   
}