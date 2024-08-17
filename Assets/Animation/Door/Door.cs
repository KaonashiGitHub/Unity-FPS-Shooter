using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        doorAnimator.SetTrigger("Open");
            }
        }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            {
                doorAnimator.SetTrigger("Closed");
            }
    }
}
