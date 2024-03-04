using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMobile : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    public Animator anim;
    private Rigidbody2D rb;
    public float bulletSpeed;
    public float force;
    public float bulletPower;
    public Vector2 joystickDirection;
    public Transform parentDirection;

   public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        parentDirection = transform.parent;
        transform.parent = null;
        Vector3 rotation = transform.position - new Vector3(joystickDirection.x, joystickDirection.y, 0);
        rb.velocity = parentDirection.up * bulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
    }

    void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
