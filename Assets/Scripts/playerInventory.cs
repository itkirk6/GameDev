using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    private HashSet<keyTypes> keys = new HashSet<keyTypes>();

    public void addKey(keyTypes key)
    {
        if (key != keyTypes.none)
        {
            keys.Add(key);
            Debug.Log("picked up key: " + key);
        }
    }

    public bool hasKey(keyTypes key)
    {
        if (key == keyTypes.none)
            return true;

        return keys.Contains(key);
    }
}