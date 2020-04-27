using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] int maxPixels = 100;
    [SerializeField] int pixels = 100;
    [SerializeField] private int expToNextLevel = 100;
    [SerializeField] private int exp = 0;
    [SerializeField] private int level = 1;

    public int Pixels {
        get {
            return pixels;
        }
        set {
            if (value > maxPixels)
                pixels = maxPixels;

            else if (value < 0)
                pixels = 0;

            else
                pixels = value;

            pixelsBar.SetHealth(pixels);
        }
    }

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    public HealthBar healthBar;
    public HealthBar pixelsBar;

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

    public bool UsePixels(int noOfPixelsUsed)
    {
        if (Pixels < noOfPixelsUsed)
        {
            return false;
        }

        Pixels -= noOfPixelsUsed;
        return true;
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
        pixels = maxPixels;
        pixelsBar.SetMaxHealth(maxPixels);
        isAlive = true;
    }

    public void AddExp(int newExp)
    {
        exp += newExp;
        if (exp >= expToNextLevel)
        {
            exp -= expToNextLevel;
            LevelUp();
            //LevelUpSound();
        }
    }

    private void LevelUp()
    {
        level++;
        health = maxHealth = (int)(1.2 * maxHealth);
        maxPixels = (int)(1.1 * maxPixels);
    }
}
