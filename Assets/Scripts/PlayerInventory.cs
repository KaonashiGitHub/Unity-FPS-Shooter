using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<string> keys = new HashSet<string>();

    public void AddKey(string key)
    {
        keys.Add(key);
    }

    public bool HasKey(string key)
    {
        return keys.Contains(key);
    }
}
