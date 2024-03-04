using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHealth : MonoBehaviour
{
    private Animator anim;
    private bool isDead;
    private float health;

    public GameObject healthPrefab;
    public GameObject goldPrefab;

    public float healthDropRate = 2f;
    public float goldDropRate = 5f;
    public void SetValues(Animator getAnimator, int getHealth)
    {
        anim = getAnimator;
        health = getHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>() !=null)
            {
            float bulletPower = collision.gameObject.GetComponent<Bullet>().bulletPower;
            TakeDamage(bulletPower);

            }
            if (collision.gameObject.GetComponent<BulletMobile>() !=null)
            {
            float bulletPower = collision.gameObject.GetComponent<BulletMobile>().bulletPower;
            TakeDamage(bulletPower);

            }
        }
    }
    private void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            anim.SetTrigger("Damage");

            if (health <= 0)
            {
                isDead = true;
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
    }
    void OnAnimationEnd()
    {
        GameManager.instance.MonsterKilled();
        DropAndDestroy();
    }
    private void DropAndDestroy()
    {
        float randomNumber = Random.Range(1f, 10f);
        EnemyManager.instance.rangedList.Remove(gameObject);
        if (randomNumber < healthDropRate)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (randomNumber < goldDropRate)
        {
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
