using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolTimeImg : MonoBehaviour
{
    public GameObject mainImg;
    public Image fill;
    public Text count;
    public float coolTime;
    public float currentCoolTime;

    public void SetCurrentCooldown(in float value)
    {
        currentCoolTime = value;
        count.text = currentCoolTime.ToString("0.0");
        UpdateFillAmount();
    }

    private void UpdateFillAmount()
    {
        fill.fillAmount = currentCoolTime / coolTime;
    }

    private void Update()
    {
        if (mainImg.activeSelf)
        {
            SetCurrentCooldown(currentCoolTime - Time.deltaTime);

            if (currentCoolTime < 0f)
            {
                currentCoolTime = coolTime;
                mainImg.SetActive(false);
            }
        }
    }
}


