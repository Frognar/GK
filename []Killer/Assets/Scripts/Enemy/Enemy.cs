using UnityEngine;
using UnityEngine.VFX;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   Anna Mach - ?
 */
public class Enemy : MonoBehaviour, ITakeDamage, IHavePiksels, IGiveExp {
    [Header ("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int health = 100;
    protected int Health {
        get {
            return health;
        }
        set {
            health = value;
            healthBar.SetValue (health);
        }
    }

    [Header ("Other")]
    public GameObject deathEffectGO;
    public GUIBar healthBar;

    [Header ("Drop")]
    [SerializeField] protected int minPixelsDrop = 5;
    [SerializeField] protected int maxPixelsDrop = 15;
    public GameObject pixelsToPickPrefab;

    [Header ("Exp")]
    [SerializeField] protected int exp = 10;

    protected SoundPlayer soundPlayer;

    protected virtual void Start () {
        healthBar.SetMaxValue (maxHealth);
        Health = maxHealth;
        soundPlayer = GetComponent<SoundPlayer> ();
        if (soundPlayer == null)
            Debug.LogWarning ("No soundPlayer in NPC [" + this.name + "]");
    }

    public void TakeDamage (int damage) {
        Health -= damage;

        if (Health <= 0)
            Die ();
        else
            soundPlayer?.PlaySoundEvent ("EnemyTakeDamage");

        if (gameObject.CompareTag ("EnemyCubeMaster"))
            GetComponent<EnemyCubeMaster> ().DestroyCube ();
    }

    protected virtual void Die () {
        CreateDeathEffect ();
        CreatePixelsToPick ();
        GiveExp ();

        soundPlayer?.PlaySoundEvent ("EnemyDie");
        transform.position = new Vector3 (0f, 0f, 0f);
        Destroy (gameObject, .1f);
    }

    protected void CreateDeathEffect () {
        GameObject deathEffect = Instantiate (deathEffectGO, transform.position + new Vector3 (0f, .5f, 0f), transform.rotation);
        VisualEffect death = deathEffect.GetComponent<VisualEffect> ();
        if (death != null) {
            MeshRenderer myRenderer = GetComponentInChildren<MeshRenderer> ();
            if (myRenderer != null) {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForEnemyDeathEffect (myRenderer.material);
                death.SetGradient ("Color", gradient);
            }

            death.Play ();
        }
        Destroy (deathEffect, 15f);
    }

    protected void CreatePixelsToPick () {
        GameObject pixelsToPixkGO = Instantiate (pixelsToPickPrefab, transform.position, Quaternion.identity);
        PixelsToPick pixelsToPick = pixelsToPixkGO.GetComponent<PixelsToPick> ();
        if (pixelsToPick != null)
            pixelsToPick.SetPixels (DropPixels());
    }

    public int DropPixels () {
        return Random.Range (minPixelsDrop, maxPixelsDrop);
    }

    public int GiveExp () {
        PlayerManager.instance.player.GetComponent<Player> ().AddExp (exp);
        return exp;
    }
}
