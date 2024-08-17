using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        else if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        else if (objectWeHit.gameObject.CompareTag("Beer"))
        {
            print("hit a beer bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
        }
        else if (objectWeHit.gameObject.CompareTag("KeyBottle"))
        {
            print("hit a key bottle");
            objectWeHit.gameObject.GetComponent<KeyBottle>().Shatter();
        }
        else if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            print("hit an enemy");
            objectWeHit.gameObject.GetComponent<EnemyMovement>().TakeDamage(25);
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        else if (objectWeHit.gameObject.CompareTag("Turret"))
        {
            print("hit a turret");
            TurretController turret = objectWeHit.gameObject.GetComponent<TurretController>();
            if (turret != null)
            {
                turret.TakeDamage(25);
            }
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}