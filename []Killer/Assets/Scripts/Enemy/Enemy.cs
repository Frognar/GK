using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour, ITakeDamage, IHavePiksels {
    [Header ("Health")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int health = 100;

    [Header ("Other")]
    public GameObject deathEffectGO;
    public HealthBar healthBar;

    [Header ("Drop")]
    [SerializeField] protected int minPixelsDrop = 5;
    [SerializeField] protected int maxPixelsDrop = 15;

    protected SoundManager soundManager;

    protected virtual void Start () {
        health = maxHealth;
        healthBar.SetMaxHealth (maxHealth);
        soundManager = SoundManager.instance;
        if (soundManager == null)
            Debug.LogWarning ("No soundManager in enemy!");
    }

    public void TakeDamage (int damage) {
        health -= damage;
        healthBar.SetHealth (health);

        if (health <= 0)
            Die ();
        else
            soundManager?.PlaySound ("EnemyTakeDamage");

        if (gameObject.CompareTag ("EnemyCubeMaster"))
            GetComponent<EnemyCubeMaster> ().DestroyCube ();
    }

    public void Die () {
        CreateDeathEffect ();
        gameObject.transform.position = new Vector3 (0f, 0f, 0f);
        Destroy (gameObject, .1f);
        DropPixels ();

        soundManager?.PlaySound ("EnemyDie");
    }

    private void CreateDeathEffect () {
        GameObject deathEffect = Instantiate (deathEffectGO, gameObject.transform.position + new Vector3 (0f, .5f, 0f), Quaternion.LookRotation (new Vector3 (0f, 0f, 0f)));
        VisualEffect death = deathEffect.GetComponent<VisualEffect> ();
        if (death != null) {
            MeshRenderer myRenderer = gameObject.GetComponentInChildren<MeshRenderer> ();
            if (myRenderer != null) {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForEnemyDeathEffect (myRenderer.material);
                death.SetGradient ("Color", gradient);
            }

            death.Play ();
        }
        Destroy (deathEffect, 15f);
    }

    public int DropPixels () {
        int Pixels = Random.Range (minPixelsDrop, maxPixelsDrop);
        PlayerManager.instance.player.GetComponent<Player> ().Pixels += Pixels;
        return Pixels;
    }
}
