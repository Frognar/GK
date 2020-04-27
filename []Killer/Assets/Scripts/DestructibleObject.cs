using UnityEngine;
using UnityEngine.VFX;

public class DestructibleObject : MonoBehaviour, ITakeDamage<int>, IHavePiksels
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 15;
    private int health = 15;
    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return health; } }

    [Header("PixelDrop")]
    [SerializeField] private int minPixelsDrop = 5;
    [SerializeField] private int maxPixelsDrop = 15;
    public int MinPixelsDrop { get { return minPixelsDrop; } }
    public int MaxPixelsDrop { get { return maxPixelsDrop; } }

    [Header("Destroy VFX")]
    public GameObject destroyEffectPrefab;
    private SoundManager soundManager;

    private void Start()
    {
        health = maxHealth;
        soundManager = SoundManager.instance;
    }
            
    public void Die()
    {
        int pixels = DropPixels();

        Debug.Log("I drop " + pixels + " pixels!");

        GameObject destroyEffectGO = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        VisualEffect destroyEffect = destroyEffectGO.GetComponent<VisualEffect>();

        if(destroyEffect != null)
        {
            MeshRenderer myRenderer = gameObject.GetComponent<MeshRenderer>();
            if(myRenderer != null)
            {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForImpactEffect(myRenderer.material);
                destroyEffect.SetGradient("Color", gradient);
            }

            destroyEffect.Play();
        }

        Destroy(destroyEffectGO, 3f);
        soundManager.PlaySound("DestroyObj");
        Destroy(gameObject, .1f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    public int DropPixels()
    {
        int Pixels = Random.Range(MinPixelsDrop, MaxPixelsDrop);
        PlayerManager.instance.player.GetComponent<Player>().Pixels += Pixels;
        return Pixels;
    }
}
