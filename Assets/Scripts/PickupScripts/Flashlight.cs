using NUnit.Framework;
using UnityEngine;


public class Flashlight: MonoBehaviour, IUsableItem
{
    private  bool isOn = true;
    public void UseItem()
    {
        Camera mainCamera = FindFirstObjectByType<Camera>();
        if(isOn)
        {
            mainCamera.orthographicSize = 7;
            isOn = false;
        }
        else
        {
            mainCamera.orthographicSize = 5;
            isOn = true;
        }
    }
}
