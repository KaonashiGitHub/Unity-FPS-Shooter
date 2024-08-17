using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;

    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Brust
    public int bulletsPerBrust = 3;
    public int burstBulletLeft;

    //Spread
    public float spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f; // seconds

    public GameObject muzzleEffect;
    internal Animator animator;

    //Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    //UI
    public TextMeshProUGUI ammoDisplay;

    public enum WeaponModel
    {
        M1911,
        M4_8
    }

    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Brust,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletLeft = bulletsPerBrust;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {
            GetComponent<Outline>().enabled = false;

            // Empty Magazine Sound
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSoundM1911.Play();
            }

            if (currentShootingMode == ShootingMode.Auto)
            {
                // Holding Down Left Mouse Button
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single ||
            currentShootingMode == ShootingMode.Brust)
            {
                //Clicking Left Mouse Button Once
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
            {
                Reload();
            }

            //If you want to automatically reload when magazine empty
            if (readyToShoot && !isShooting && isReloading == false && bulletsLeft <= 0)
            {
                Reload();
            }


            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletLeft = bulletsPerBrust;
                FireWeapon();
            }

            if (AmmoManager.Instance.ammoDisplay != null)
            {
                AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBrust}/{magazineSize / bulletsPerBrust}";
            }
        }

    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        // SoundManager.Instance.shootingSoundM1911.Play();

        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDicrection = CalculateDirectionAndSpread().normalized;
        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //Poiting the bullet to face the shooting direction
        bullet.transform.forward = shootingDicrection;
        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDicrection * bulletVelocity, ForceMode.Impulse);
        //Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        //Checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        //Brust Mode
        if (currentShootingMode == ShootingMode.Brust && burstBulletLeft > 1) //We already shoot once before this check 
        {
            burstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            //Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
