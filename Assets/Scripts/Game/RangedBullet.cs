using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBullet : MonoBehaviour
{
   public Animator anim;
    private GameObject player;
    private Rigidbody2D rb;
    public float bulletSpeed;
    public float force;
    public int bulletPower;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
        Vector3 direction = player.transform.position - transform.position;
        Vector3 rotation = transform.position - player.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
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
