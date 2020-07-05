using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   Anna Mach - system pikseli, potrzebne do zapisu danych gettery i settery i zmiany
 *                  Mateusz Chłopek - system levelowania i umiejętności
 */
public class Player : MonoBehaviour, ITakeDamage {
    private MouseInput mouseInput;
    private KeyboardInput keyboardInput;
    private SoundPlayer soundPlayer;

    private Dictionary<char, Ability> abilities;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] int maxPixels = 100;
    [SerializeField] int pixels = 100;
    [SerializeField] private int expToNextLevel = 100;
    [SerializeField] private int exp;
    [SerializeField] private int level;

    public GameObject saveLoadData;

    public int Level { get { return level; } set { level = value; } }

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
    public Text healthRecoverCdText;
    public Text ammoRecoverCdText;

    private void Awake () {
        mouseInput = GetComponent<MouseInput> ();
        keyboardInput = GetComponent<KeyboardInput> ();
        soundPlayer = GetComponent<SoundPlayer> ();
    }

    void Start () {
        ResetPlayerWithoutExp();
    }

    void Update()
    {
        foreach(KeyValuePair <char, Ability> ability in abilities)
        {
            ability.Value.Update(Time.deltaTime);
        }
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
    
    public void ResetPlayerWithoutExp()
    {

        healthBar.SetMaxValue(maxHealth);
        pixelsBar.SetMaxValue(maxPixels);
        expBar.SetMaxValue(expToNextLevel);

        Health = maxHealth;
        Pixels = maxPixels;

        InputEnabled(true);
        isAlive = true;

        abilities = new Dictionary<char, Ability>();
        abilities['1'] = new RecoverHealth(this);
        abilities['2'] = new RecoverPixels(this);
    }

    public void ResetPlayer () {
        ResetPlayerWithoutExp();
        Exp = 0;

        saveLoadData.GetComponent<DataToSaveLoad>().IncreaseMaxEnemiesAttacking();
    }

    public void UseAbility(char pressedKey)
    {
        abilities[pressedKey].UseAbility();
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
