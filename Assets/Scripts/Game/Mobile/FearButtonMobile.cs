using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearButtonMobile : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fearFX;
    public Image fillImage;
    public float fillTime = 1.5f;
    private bool canClick;
    private void Start()
    {
        StartCoroutine(FillCoroutine());

}
    public void fearStartSound()
    {
        audioSource.PlayOneShot(fearFX);
    }
       
    private IEnumerator FillCoroutine()
    {
        float elapsedTime = 0.0f;
        float fillAmount = 0.0f;

        while (fillAmount < 1.0f)
        {
            fillAmount = Mathf.Min(1.0f, elapsedTime / fillTime);
            fillImage.fillAmount = fillAmount;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        canClick = true;
    }

    public void FearFill()
    {
        if (canClick)
        {
            fearStartSound();
            StartCoroutine(FillCoroutine());
            canClick = false;
        }
      
    }
}
