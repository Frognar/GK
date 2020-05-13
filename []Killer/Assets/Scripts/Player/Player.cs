using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   Anna Mach - system pikseli
 *                  Mateusz Chłopek - system levelowania
 */
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

            pixelsBar.SetValue (pixels);
        }
    }

    private void SetMaxPixels (int maxPixels) {
        this.maxPixels = maxPixels;
        this.pixelsBar.SetMaxValue (maxPixels);
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

            healthBar.SetValue (health);
        }
    }

    private void SetMaxHealth (int maxHealth) {
        this.maxHealth = maxHealth;
        this.healthBar.SetMaxValue (maxHealth);
    }

    public int Exp {
        get {
            return exp;
        }
        set {
            exp = value;

            expBar.SetValue (exp);
        }
    }

    private void SetExpToNextLevel (int expToNextLevel) {
        this.expToNextLevel = expToNextLevel;
        this.expBar.SetMaxValue (expToNextLevel);
    }

    private bool isAlive = true;
    public bool IsAlive {
        get {
            return isAlive;
        }
    }

    public GUIBar healthBar;
    public GUIBar pixelsBar;
    public GUIBar expBar;

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
            Health -= damage;

            if (Health <= 0)
                Die ();
            else
                soundPlayer.PlaySoundEvent ("PlayerTakeDamage");
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
        InputEnabled (false);
        isAlive = false;
    }

    public void ResetPlayer () {
        healthBar.SetMaxValue (maxHealth);
        pixelsBar.SetMaxValue (maxPixels);
        expBar.SetMaxValue (expToNextLevel);

        Health = maxHealth;
        Pixels = maxPixels;
        Exp = 0;
        InputEnabled (true);
        isAlive = true;
    }

    public void AddExp (int newExp) {
        Exp += newExp;
        if (Exp >= expToNextLevel)
            LevelUp ();
    }

    private void LevelUp () {
        level++;
        Exp -= expToNextLevel;

        SetMaxHealth ((int) (1.2f * maxHealth));
        SetMaxPixels ((int) (1.1f * maxPixels));
        SetExpToNextLevel ((int) (1.2f * expToNextLevel));
        Health = maxHealth;

        soundPlayer.PlaySoundEventOnDefaultSource ("LevelUp");
    }

    public void InputEnabled (bool enabled) {
        mouseInput.enabled = enabled;
        keyboardInput.enabled = enabled;
    }
}
