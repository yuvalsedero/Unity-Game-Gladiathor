using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public StatsScriptableObject characterStats;
    public GameObject healthBarUI;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        characterStats.health = characterStats.maxHealth;
        slider.value = (CalculateHealth() / 100);
        healthBarUI.SetActive(true);
    }
  
    // Update is called once per frame
    void Update()
    {
        if (characterStats.health > characterStats.maxHealth)// just so he wont have more hp then the max amount
        {
            characterStats.health = characterStats.maxHealth;
        }
        slider.value = (CalculateHealth() / 100);
    }
    float CalculateHealth()
    {
        return characterStats.health;
    }
    public void pickUpHp()
    {
        characterStats.health += 10;
    }
}
