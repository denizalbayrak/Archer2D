using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedMovement : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    public float distanceBetween;
    public float fightDistance;

    private Animator anim;
    private GameObject player;
    private float distance;
    private float cooldownTimer = Mathf.Infinity;
    public bool playerInsight;
    public bool isFearActive;
    private float speed;

    public GameObject bullet;

    public void SetValues(float getSpeed, Animator getAnimator, GameObject getPlayer)
    {
        speed = getSpeed;
        anim = getAnimator;
        player = getPlayer;
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (!isFearActive)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance > distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }

            playerInsight = distance < fightDistance;

            cooldownTimer += Time.deltaTime;
            if (playerInsight && cooldownTimer >= attackCooldown)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            EnemyFear();
        }

    }

    public void EnemyFear()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        Vector2 reversedDirection = -directionToPlayer.normalized;
        transform.Translate(reversedDirection * Time.deltaTime * speed);
    }
   
}
