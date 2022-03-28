using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    HealthPlayerScript playerHealth;
    Slider healthBar;
    void Start()
    {
        healthBar = GetComponent<Slider>();
        playerHealth = GameObject.Find("Player").GetComponent<HealthPlayerScript>();
    }

    void Update()
    {
        healthBar.maxValue = playerHealth.GetMaxHealth();
        healthBar.value = playerHealth.GetHealth();
    }
}
