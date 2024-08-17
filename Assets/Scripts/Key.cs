using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private string keyName; // Nhập tên chìa khóa trong Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddKey(keyName);
                Destroy(gameObject); // Xóa chìa khóa khỏi scene
            }
        }
    }
}
