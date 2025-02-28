using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Weapon;

public class Weapon : MonoBehaviour
{


    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;


    //spread
    public float spreadIntersity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    private Animator animator;

    //loading 
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public enum WeaponModel
    {
        Pistol1911,
        M16
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
        burstBulletsLeft = bulletsPerBurst;
        bulletsLeft = magazineSize;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletsLeft == 0 && isShooting )
        {
            SoundManager.Instance.emptyMagazineSound1911.Play();
        }

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Brust)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting && bulletsLeft>0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }


        if (AmmoManager.Instance.ammoDisplay != null) 
        {
            AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();

        }

        //if you want to automatically reload when magazine is empty
        //if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0) { Reload(); }
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        //SoundManager.Instance.ShootingChannel.Play();
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);


        //pointing the bullet to face the shooting directing
        bullet.transform.forward = shootingDirection;


        //shoot a bullet
        Rigidbody BulletRigidbody = bullet.GetComponent<Rigidbody>();
        BulletRigidbody.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        //Destroy the bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));


        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;

        }

        if (currentShootingMode == ShootingMode.Brust && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }

    }

    private void Reload()
    {
        animator.SetTrigger("RELOAD");
        //SoundManager.Instance.reloadingSound1911.Play();
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
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

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //hitting Something
            targetPoint = hit.point;
        }
        else
        {
            //shooting in air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntersity, spreadIntersity);
        float y = UnityEngine.Random.Range(-spreadIntersity, spreadIntersity);

        //returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet);

    }
}
