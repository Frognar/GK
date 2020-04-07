using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
        else
            FindObjectOfType<AudioManager>().Play("PlayerTakenDamage");

        healthBar.SetHealth(currentHealth);
    }

    private void Death()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }
}
