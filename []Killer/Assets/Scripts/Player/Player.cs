using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IKillable<int>
{
    #region Singleton
    public static Player instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region Zmianne
    #region IKillable
    [SerializeField] private int m_maxHealth = 100;
    [SerializeField] private int m_health = 100;
    public int maxHealth { get { return m_maxHealth; } set { m_maxHealth = value; } }
    public int health { get { return m_health; } set { m_health = value; } }
    #endregion

    private bool m_isAlive = true;
    public bool isAlive { get { return m_isAlive; } set { m_isAlive = value; } }

    private Transform spawnPosition;
    public HealthBar healthBar;
    #endregion

    void Start()
    {
        spawnPosition = GameManager.instance.playerSpawn;
        health = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        transform.position = spawnPosition.position;
    }

    #region IKillable
    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;

            if (health <= 0)
                Die();
            else
                AudioManager.instance.Play("PlayerTakeDamage");

            healthBar.SetHealth(health);
        }
    }

    public void Die()
    {
        AudioManager.instance.Play("PlayerDeath");
        isAlive = false;
        StartCoroutine(Respawn());
    }
    #endregion

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = GameManager.instance.playerSpawn.position;
        yield return new WaitForSeconds(.2f);
        health = maxHealth;
        healthBar.SetHealth(health);
        isAlive = true;
    }

}
