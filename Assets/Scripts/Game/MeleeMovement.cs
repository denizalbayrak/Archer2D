using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    public GameObject weapons;
    public float distanceBetween;
    public float fightDistance;

    private Animator anim;
    private GameObject player;
    private float distance;
    private float cooldownTimer = Mathf.Infinity;
    private bool playerInsight;
    public bool isFearActive;
    private float speed;
 
    void Start()
    {
        if (weapons == null)
        {
            Debug.LogError("Weapons reference is not set in MeleeMovement script!");
        }
    }
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
            StartCoroutine(Attack());
        }
    }
        else
        {
            EnemyFear();
        }
    }

    IEnumerator Attack()
    {
        cooldownTimer = 0;
        anim.SetTrigger("Attack");
        weapons.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        weapons.SetActive(false);
    }
    public void EnemyFear()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        Vector2 reversedDirection = -directionToPlayer.normalized;
        transform.Translate(reversedDirection * Time.deltaTime * speed);
    }
   

}
