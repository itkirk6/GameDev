using UnityEngine;
using UnityEngine.EventSystems;

public class MenuTextHoverSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  
{ 
    [SerializeField] private GameObject normalText;  
    [SerializeField] private GameObject hoverText;  
  
    private void Start()  
    {
        if (normalText != null) normalText.SetActive(true);  
        if (hoverText != null) hoverText.SetActive(false);  
    }

    public void OnPointerEnter(PointerEventData eventData)  
    {
        if (normalText != null) normalText.SetActive(false);  
        if (hoverText != null) hoverText.SetActive(true);  
    }

    public void OnPointerExit(PointerEventData eventData)  
    { 
        if (normalText != null) normalText.SetActive(true);
        if (hoverText != null) hoverText.SetActive(false); 
    }
}