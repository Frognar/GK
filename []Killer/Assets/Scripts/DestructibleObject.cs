using UnityEngine;
using UnityEngine.VFX;

public class DestructibleObject : MonoBehaviour, ITakeDamage, IHavePiksels
{
    [Header("Health")]
    [SerializeField] private int health = 15;

    [Header("PixelDrop")]
    [SerializeField] private int minPixelsDrop = 5;
    [SerializeField] private int maxPixelsDrop = 15;
    public GameObject pixelsToPickPrefab;

    [Header("Destroy VFX")]
    public GameObject destroyEffectPrefab;
    private SoundPlayer soundPlayer;

    private void Start () {
        soundPlayer = GetComponent<SoundPlayer> ();
    }

    private void DestroyObject () {
        CreateDestroyEffect ();
        CreatePixelsToPick ();
        Destroy (gameObject, .1f);
    }

    private void CreateDestroyEffect () {
        GameObject destroyEffectGO = Instantiate (destroyEffectPrefab, transform.position, Quaternion.identity);
        VisualEffect destroyEffect = destroyEffectGO.GetComponent<VisualEffect> ();
        if (destroyEffect != null) {
            MeshRenderer myRenderer = gameObject.GetComponent<MeshRenderer> ();
            if (myRenderer != null) {
                Gradient gradient = MaterialsColorManager.instance.NiceGradientForImpactEffect (myRenderer.material);
                destroyEffect.SetGradient ("Color", gradient);
            }

            destroyEffect.Play ();
        }

        Destroy (destroyEffectGO, 3f);
        soundPlayer.PlaySoundEvent ("DestroyObj");
    }

    public void TakeDamage (int damage) {
        health -= damage;

        if (health <= 0)
            DestroyObject ();
    }

    private void CreatePixelsToPick () {
        GameObject pixelsToPixkGO = Instantiate (pixelsToPickPrefab, transform.position, Quaternion.identity);
        PixelsToPick pixelsToPick = pixelsToPixkGO.GetComponent<PixelsToPick> ();
        if (pixelsToPick != null)
            pixelsToPick.SetPixels (DropPixels ());
    }

    public int DropPixels () {
        return Random.Range (minPixelsDrop, maxPixelsDrop);
    }
}
