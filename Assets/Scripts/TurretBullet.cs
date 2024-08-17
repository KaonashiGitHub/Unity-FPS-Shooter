using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float damage = 10f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Hit the player!");

            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }
}
