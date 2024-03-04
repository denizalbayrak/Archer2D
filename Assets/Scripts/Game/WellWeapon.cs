using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellWeapon : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public float fillTime = 5f;
    public bool isWellWeapon;
   public bool isFired = true;

    public float minValue = 2f;
    public float maxValue = 8f; 
    public float currentValue = 2f;
    public float changeSpeed = 5f;
    bool increasing = true;

    private void Start()
    {
        slider.value = minValue;
        WeaponFill();
    }
    void Update()
    {
        if (isWellWeapon && !isFired)
        {
            if (increasing)
            {
                currentValue += changeSpeed * Time.deltaTime;
                if (currentValue >= maxValue)
                {
                    currentValue = maxValue;
                    increasing = false;
                }
            }
            else
            {
                currentValue -= changeSpeed * Time.deltaTime;
                if (currentValue <= minValue)
                {
                    currentValue = minValue;
                    increasing = true;
                }
            }

            slider.value = currentValue;
        }       
    }

    public void ActivateSlider()
    {
        isFired = false;
        currentValue = minValue;
        slider.value = currentValue;
        slider.gameObject.SetActive(true);
    }  
    public void DeactivateSlider()
    {
        isFired = true; 
        currentValue = minValue;
        slider.value = currentValue;
        slider.gameObject.SetActive(false);
    
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
        isWellWeapon = true;
    }

    public void WeaponFill()
    {
        StartCoroutine(FillCoroutine());
    }
}
