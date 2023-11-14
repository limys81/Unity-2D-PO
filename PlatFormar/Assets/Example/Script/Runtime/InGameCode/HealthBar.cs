using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;
   // Damageable bossMosterDamageable;

    private void Awake()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
       // GameObject BossMonster = GameObject.FindGameObjectWithTag("BossMonster");

        playerDamageable = Player.GetComponent<Damageable>();
       // bossMosterDamageable = BossMonster.GetComponent<Damageable>();
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnHealthChanged);
       // bossMosterDamageable.healthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnHealthChanged);
       // bossMosterDamageable.healthChanged.RemoveListener(OnHealthChanged);
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
