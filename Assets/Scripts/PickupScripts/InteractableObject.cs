using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public float hoverMultiplier = 1.1f;
    public UnityEvent onInteract;
    public Vector3 originalScale;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if(isHovering && Input.GetKeyDown(KeyCode.F))
            Interact();
    }

    private void OnMouseEnter()
    {
        isHovering = true;
        transform.localScale = originalScale * hoverMultiplier;
    }

    private void OnMouseExit()
    {
        isHovering = false;
        transform.localScale = originalScale;
    }

    private void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}!");
        onInteract.Invoke();
    }
}
