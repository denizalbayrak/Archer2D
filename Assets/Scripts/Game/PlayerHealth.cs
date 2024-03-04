using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class PlayerHealth : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip dieFX;
    public AudioClip damagedFX;
    public AudioClip lifeFX;

    public CinemachineVirtualCamera  vCam;
    public CinemachineBasicMultiChannelPerlin noisePerlin;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rb;
    public Image[] hearts;
    public int health = 4;
    public int maxLife = 4;
    public int injuredShieldDuration;

    private bool isShieldActive;
    public float vibrationDuration = 0.5f;
    void Start()
    {
        noisePerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public float detectionRange = 0.7f;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("EnemyBullet"))
            {
                PlayerInjured();
            }
            if (collider.gameObject.CompareTag("Health"))
            {
                Destroy(collider.gameObject);
                HealthDrop();
            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
    //    {
    //        PlayerInjured();
    //    } 
    //    if (collision.gameObject.CompareTag("Health"))
    //    {
    //        Destroy(collision.gameObject);
    //        HealthDrop();
    //    }       
    //} 
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
    //    {
    //        PlayerInjured();
    //    } 
    //    if (collision.gameObject.CompareTag("Health"))
    //    {
    //        Destroy(collision.gameObject);
    //        HealthDrop();
    //    }       
    //}
    private void PlayerInjured()
    {
        if (!isShieldActive)
        {
            DamagedSound();
            isShieldActive = true;
            health--;
            hearts[health].gameObject.SetActive(false);
            animator.SetTrigger("Injure");           
           
            StartCoroutine(ShakeCamera());
            if (health <= 0)
            {
                DieSound();
                rb.bodyType = RigidbodyType2D.Static;
                if (gameObject.GetComponent<PlayerMovement>() != null)
                {
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                }
                if(gameObject.GetComponent<PlayerMovementMobile>() != null)
                {
                    gameObject.GetComponent<PlayerMovementMobile>().enabled = false;
                }
                animator.SetTrigger("Die");
                GameManager.instance.GameOver();
                return;
            }
            StartCoroutine(Injured());
        }
        
    }
  
    IEnumerator ShakeCamera()
    {
    noisePerlin.m_AmplitudeGain = 2;
    noisePerlin.m_FrequencyGain= 2;
        yield return new WaitForSeconds(0.3f);
        noisePerlin.m_AmplitudeGain = 0;
        noisePerlin.m_FrequencyGain = 0;
    }

    public void DieSound()
    {
        audioSource.PlayOneShot(dieFX);
    }
    public void DamagedSound()
    {
        audioSource.PlayOneShot(damagedFX);
    } 
    public void LifeCollectedSound()
    {
        audioSource.PlayOneShot(lifeFX);
    } 
   
    private void HealthDrop()
    {
        LifeCollectedSound();
        if (health <= maxLife-1)
        {
            health++;
            hearts[health-1].gameObject.SetActive(true);
        }
    }  
   
    IEnumerator Injured()
    {
        float endTime = Time.time + injuredShieldDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        isShieldActive = false;
        spriteRenderer.enabled = true;
    }
}
