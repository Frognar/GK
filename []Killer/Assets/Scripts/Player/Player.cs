using System;
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
        if (!isAlive)
            return;

        health -= damage;

        if (health <= 0)
            Die();
        else
            FindObjectOfType<AudioManager>().Play("PlayerTakeDamage");

        healthBar.SetHealth(health);
    }

    public void Die()
    {
        isAlive = false;
        deathPanel.SetActive(true);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        StartCoroutine(Respawn());
    }
    // -------------------------------
    public HealthBar healthBar;
    public Transform spawn;
    public GameObject deathPanel;
    public bool isAlive = true;

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

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        deathPanel.SetActive(false);
        gameObject.transform.position = spawn.position;
        yield return new WaitForSeconds(.2f);
        health = maxHealth;
        healthBar.SetHealth(health);
        isAlive = true;
    }

}
