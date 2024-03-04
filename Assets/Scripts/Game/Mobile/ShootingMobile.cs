using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingMobile : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootFX;
    public Vector3 mousePos;
    Vector2 joystickDirection;
    Vector2 joystickDirectionBullet;
    public Joystick movementJoystick;

    public GameObject bullet;
    public GameObject Player;
    public GameObject bulletWellWeapon;
    public Transform bulletTransform;
    bool wellFired;
    bool sliderActivated;
    float wellWeaponBulletSpeed;
    public bool canFire;
    public bool isButtonHolding;
    public bool isButtonUp;

    private float timer;
    public float timeBetweenFiring;

    public WellWeapon weapon;

    private void Update()
    {
        joystickDirection = new Vector2(movementJoystick.Direction.x, movementJoystick.Direction.y).normalized;
        joystickDirectionBullet = new Vector2(movementJoystick.Direction.x, movementJoystick.Direction.y).normalized;
        if (joystickDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(joystickDirection.y, joystickDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

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
    }

    public void NormalWeaponFired()
    {
        if (canFire)
        {
            ShootSound();
            canFire = false;
            var bulletNew = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            bulletNew.transform.parent = transform;
            bulletNew.GetComponent<BulletMobile>().joystickDirection = joystickDirectionBullet;
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
            if (isButtonHolding && canFire)
            {
                if (!sliderActivated)
                {
                    sliderActivated = true;
                    weapon.ActivateSlider();
                }
            }
            if (isButtonUp && canFire)
            {
                isButtonUp = false;
                wellWeaponBulletSpeed = weapon.currentValue * 10;
                sliderActivated = false;
                weapon.DeactivateSlider();
                wellFired = true;
                canFire = false;
                StartCoroutine(WellWeaponOn());
            }
        }

    }
    public void ButtonDown()
    {
        if (weapon.isWellWeapon)
        {
            isButtonHolding = true;

        }
    }
    public void ButtonUp()
    {
        if (weapon.isWellWeapon)
        {
            isButtonHolding = false;
            isButtonUp = true;

        }
    }
    IEnumerator WellWeaponOn()
    {
        for (int i = 0; i < 10; i++)
        {
            ShootSound();
            var bullet = Instantiate(bulletWellWeapon, bulletTransform.position, Quaternion.identity);
            bullet.GetComponent<BulletMobile>().joystickDirection = joystickDirectionBullet;
            bullet.transform.parent = transform;
            bullet.GetComponent<BulletMobile>().bulletSpeed = wellWeaponBulletSpeed;
            yield return new WaitForSeconds(0.15f);
        }
        canFire = true;
        wellFired = false;
        weapon.isWellWeapon = false;
        weapon.WeaponFill();
        isButtonUp = false;

    }

    public void ShootSound()
    {
        audioSource.PlayOneShot(shootFX);
    }
}
