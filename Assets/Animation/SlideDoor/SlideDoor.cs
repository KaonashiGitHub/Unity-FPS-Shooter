using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    private Animator doorAnimator;
    [SerializeField] private string requiredKey; // Nhập tên chìa khóa cần thiết trong Inspector

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null && playerInventory.HasKey(requiredKey))
            {
                doorAnimator.SetTrigger("Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("Closed");
        }
    }
}
