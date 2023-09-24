using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float speed = 5f;
    public int level = 1;
    public Animator animator;
    // Start is called before the first frame update
    private void Start()
        {
        // Set the initial health to the maximum health
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        // Reduce health based on the damage taken
        currentHealth -= damage;

        // Check if the character is dead
        if (currentHealth <= 0 && animator !=null)
                    // handle death hereif (animator != null)
        {
            animator.SetTrigger("Dead");
        }
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }
    // public void LevelUp()
    // {
    //     level++;
    //     // You can add more logic here, such as increasing health, speed, or unlocking new abilities
    // }
}
