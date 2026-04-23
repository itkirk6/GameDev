using UnityEngine;
using UnityEngine.Rendering;
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 shootDirection = (mousePos - firedFrom.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg + 90;
        Quaternion bulletRotation = Quaternion.Euler(0,0, angle);
        
        GameObject bullet = Instantiate(bulletPrefab, firedFrom.position, bulletRotation);

        BulletLogic bulletScript = bullet.GetComponent<BulletLogic>();
        if(bulletScript != null)
        {
            bulletScript.Shoot(shootDirection);
        }
    }   
}