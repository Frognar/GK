using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage {
    private MouseInput mouseInput;
    private KeyboardInput keyboardInput;
    private SoundPlayer soundPlayer;

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

            pixelsBar.SetHealth (pixels);
        }
    }

    public int Health {
        get {
            return health;
        }
        set {
            if (value > maxHealth)
                health = maxHealth;
            else
                health = value;

            healthBar.SetHealth (health);
        }
    }

    private bool isAlive = true;
    public bool IsAlive {
        get {
            return isAlive;
        }
    }

    public HealthBar healthBar;
    public HealthBar pixelsBar;

    private void Awake () {
        mouseInput = GetComponent<MouseInput> ();
        keyboardInput = GetComponent<KeyboardInput> ();
        soundPlayer = GetComponent<SoundPlayer> ();
    }

    void Start () {
        ResetPlayer ();
    }

    public void TakeDamage (int damage) {
        if (IsAlive) {
            health -= damage;

            if (health <= 0)
                Die ();
            else
                soundPlayer.PlaySoundEvent ("PlayerTakeDamage");

            healthBar.SetHealth (health);
        }
    }

    public bool UsePixels (int noOfPixelsUsed) {
        if (Pixels < noOfPixelsUsed) {
            return false;
        }

        Pixels -= noOfPixelsUsed;
        return true;
    }

    private void Die () {
        soundPlayer.PlaySoundEvent ("PlayerDie");
        mouseInput.enabled = false;
        keyboardInput.enabled = false;
        isAlive = false;
    }

    public void ResetPlayer () {
        health = maxHealth;
        healthBar.SetMaxHealth (maxHealth);
        pixels = maxPixels;
        pixelsBar.SetMaxHealth (maxPixels);
        mouseInput.enabled = true;
        keyboardInput.enabled = true;
        isAlive = true;
    }

    public void AddExp (int newExp) {
        exp += newExp;
        if (exp >= expToNextLevel) {
            exp -= expToNextLevel;
            LevelUp ();
        }
    }

    private void LevelUp () {
        level++;
        health = maxHealth = (int) (1.2 * maxHealth);
        maxPixels = (int) (1.1 * maxPixels);
        soundPlayer.PlaySoundEventOnDefaultSource ("LevelUp");
    }
}
