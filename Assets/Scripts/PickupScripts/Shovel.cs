using UnityEngine;

public class Shovel : MonoBehaviour, IUsableItem
{
    public GameObject wireCutterPrefab;

    public void UseItem()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Collider2D grassCollider = Physics2D.OverlapPoint(mousePos2D);

        if(grassCollider != null)
        {
            if(grassCollider.CompareTag("GrassPatch"))
            {
                Vector3 spawnPos = grassCollider.transform.position;
                Destroy(grassCollider.gameObject);
                if(grassCollider.GetComponent<HidingWireCutters>().isHidingWireCutters)
                {
                    Instantiate(wireCutterPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
                    Debug.Log("Shovel was used to dig up wire cutters from grass patch.");
                }
                Debug.Log("You dug up a grass patch");
            }
        }
        else
            Debug.Log("Shovel can only be used on a grass patch.");
    }
    
}
