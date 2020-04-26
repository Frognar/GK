using UnityEngine;
using UnityEngine.VFX;

public class DestructibleObject : MonoBehaviour, ITakeDamage<int>
{
    private int maxHealth = 15;
    private int health = 15;
    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return health; } }

    public GameObject destroyEffectPrefab;
    private SoundManager soundManager;

    private void Start()
    {
        health = maxHealth;
        soundManager = SoundManager.instance;
    }
            
    public void Die()
    {
        GameObject destroyEffectGO = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        VisualEffect destroyEffect = destroyEffectGO.GetComponent<VisualEffect>();

        Debug.Log("Obcject Die");

        if(destroyEffect != null)
        {
            Debug.Log("Obcject destroyEffect");
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
}
