using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();
    public GameObject keyPrefab; // Prefab của chìa khóa
    public Vector3 keySpawnOffset; // Vị trí spawn của chìa khóa so với chai

    public void Shatter()
    {
        // Phá vỡ chai bằng cách vô hiệu hóa isKinematic của tất cả các phần
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }

        // Tạo ra chìa khóa tại vị trí chai bị phá vỡ
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position + keySpawnOffset, Quaternion.identity);
        }

        Destroy(gameObject, 3f);
    }
}