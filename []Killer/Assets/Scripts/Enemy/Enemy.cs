﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour, ITakeDamage<int>, IHavePiksels
{
    [Header("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int health = 100;
    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return health; } }

    private bool unmobilzed = false;
    public bool Unmobilized { get { return unmobilzed; } set { unmobilzed = value; } }

    [Header("Other")]
    public GameObject deathEffectGO;
    public HealthBar healthBar;

    protected SoundManager soundManager;

    [Header("Drop")]
    [SerializeField] protected int minPixelsDrop = 5;
    [SerializeField] protected int maxPixelsDrop = 15;
    public int MinPixelsDrop { get { return minPixelsDrop; } }
    public int MaxPixelsDrop { get { return maxPixelsDrop; } }

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        soundManager = SoundManager.instance;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
            Die();
        else
            soundManager.PlaySound("EnemyTakeDamage");

        if (gameObject.tag == "EnemyCubeMaster")
            GetComponent<EnemyCubeMaster>().DestroyCube();
    }

    public void Die()
    {
        GameObject deathEffect = Instantiate(deathEffectGO, gameObject.transform.position + new Vector3(0f, .5f, 0f), Quaternion.LookRotation(new Vector3(0f, 0f, 0f)));
        VisualEffect death = deathEffect.GetComponent<VisualEffect>();
        if (death != null)
        {
            MeshRenderer myRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
            if (myRenderer != null)
            {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForEnemyDeathEffect(myRenderer.material);
                death.SetGradient("Color", gradient);
            }

            death.SetVector3("Scale", transform.localScale);

            death.Play();
        }
        Destroy(deathEffect, 15f);
        gameObject.transform.position = new Vector3(0f, 0f, 0f);
        Destroy(gameObject, .1f);
        DropPixels();
        soundManager.PlaySound("EnemyDie");
    }

    public int DropPixels()
    {
        int Pixels = Random.Range(MinPixelsDrop, MaxPixelsDrop);
        PlayerManager.instance.player.GetComponent<Player>().Pixels += Pixels;
        return Pixels;
    }
}
