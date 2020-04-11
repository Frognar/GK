using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    public HealthBar healthBar;

    void Start()
    {
        ResetPlayer();
    }

    public void TakeDamage(int damage)
    {
        if (IsAlive)
        {
            health -= damage;

            if (health <= 0)
                Die();
            else
                GetComponent<SoundPlayer>().PlaySoundEvent("PlayerTakeDamage");

            healthBar.SetHealth(health);
        }
    }

    private void Die()
    {
        GetComponent<SoundPlayer>().PlaySoundEvent("PlayerDie");
        isAlive = false;
    }

    public void ResetPlayer()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isAlive = true;
    }

}
