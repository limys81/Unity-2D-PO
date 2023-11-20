using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable bossDamageable;

    private void Awake()
    { 
        GameObject BossMonster = GameObject.FindGameObjectWithTag("BossMonster");

        bossDamageable = BossMonster.GetComponent<Damageable>();
    }   

    private void Start()
    {
        healthSlider.value = CalculateSliderPercentage(bossDamageable.Health, bossDamageable.MaxHealth);
        healthBarText.text = "HP " + bossDamageable.Health + " / " + bossDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        bossDamageable.healthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        bossDamageable.healthChanged.RemoveListener(OnHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + " / " + maxHealth;
    }
}
