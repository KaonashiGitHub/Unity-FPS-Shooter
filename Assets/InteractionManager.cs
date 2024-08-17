using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }
    public Weapon hoveredWeapon = null;
    private void Awake()
    {
        if(Instance != null && Instance != this )
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }   

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new UnityEngine.Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRayCast = hit.transform.gameObject;
            Debug.Log(objectHitByRayCast);

            if(objectHitByRayCast.GetComponent<Weapon>())
            { 
                print("Weapon Selected");
                hoveredWeapon = objectHitByRayCast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if(Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRayCast.gameObject);
                }
            }
            else
            {
                if(hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}