using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootFX;
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public GameObject bulletWellWeapon;
    public Transform bulletTransform;
    bool wellFired;
    float wellWeaponBulletSpeed;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    public WellWeapon weapon;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);

        if (weapon.isWellWeapon)
        {
            WellWeapon();
            return;
        }
        NormalWeapon();
    }

    void NormalWeapon()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButtonUp(0) && canFire)
        {
            ShootSound();
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
    void WellWeapon()
    {
        if (!wellFired)
        {
            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer > timeBetweenFiring)
                {
                    canFire = true;
                    timer = 0;
                }
            }
            if (Input.GetMouseButtonDown(0) && canFire)
            {
                weapon.ActivateSlider();
            }
            if (Input.GetMouseButtonUp(0) && canFire)
            {
              
                wellWeaponBulletSpeed = weapon.currentValue*10;
                weapon.DeactivateSlider();
                wellFired = true;
                canFire = false;
                StartCoroutine(WellWeaponOn());
            }
        }
        
    }

    IEnumerator WellWeaponOn()
    {
        for (int i = 0; i < 10; i++)
        {
            ShootSound();
            var bullet = Instantiate(bulletWellWeapon, bulletTransform.position, Quaternion.identity);
          bullet.GetComponent<Bullet>().bulletSpeed = wellWeaponBulletSpeed;
          yield return new WaitForSeconds(0.15f);
        }
        canFire = true;
        wellFired = false;
        weapon.isWellWeapon = false;
        weapon.WeaponFill();

    }

    public void ShootSound()
    {
        audioSource.PlayOneShot(shootFX);
    }
}
