using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip goldFX;
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Gold"))
        {
            Destroy(collision.gameObject);
            GoldDrop();
        }
    }
    private void GoldDrop()
    {
        GoldCollectedSound();
        GameManager.instance.ItemCollected();
    }
    public void GoldCollectedSound()
    {
        audioSource.PlayOneShot(goldFX);
    }
}
