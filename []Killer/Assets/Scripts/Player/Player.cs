using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable<int>
{
    // -----------IKillable-----------
    public int health { get; set; }
    public int maxHealth { get; set; }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
        else
            FindObjectOfType<AudioManager>().Play("EnemyTakeDamage");

        healthBar.SetHealth(health);
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Respawn();
    }
    // -------------------------------
    public HealthBar healthBar;
    public Transform spawn;

    Player()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        gameObject.transform.position = spawn.position;
    }

    void Respawn()
    {
        health = maxHealth;
        gameObject.transform.position = spawn.position;
    }

}
