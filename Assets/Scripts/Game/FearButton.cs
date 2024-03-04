using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fearFX;
    public Image fillImage;
    public float fillTime = 1.5f;
    private bool canClick;
    private void Start()
    {
        FearFill();
    }
    public void fearStartSound()
    {
        audioSource.PlayOneShot(fearFX);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteButtonClick();
        }
    }
    private void ExecuteButtonClick()
    {
        Button button = GetComponent<Button>();
        if (button != null && canClick)
        {
            fearStartSound();
            button.onClick.Invoke();
            canClick = false;
        }

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
        StartCoroutine(FillCoroutine());
    }

}
