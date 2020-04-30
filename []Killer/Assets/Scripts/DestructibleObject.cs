using UnityEngine;
using UnityEngine.VFX;

public class DestructibleObject : MonoBehaviour, ITakeDamage, IHavePiksels
{
    [Header("Health")]
    [SerializeField] private int health = 15;

    [Header("PixelDrop")]
    [SerializeField] private int minPixelsDrop = 5;
    [SerializeField] private int maxPixelsDrop = 15;

    [Header("Destroy VFX")]
    public GameObject destroyEffectPrefab;
    private SoundPlayer soundPlayer;

    private void Start () {
        soundPlayer = GetComponent<SoundPlayer> ();
    }

    private void DestroyObject () {
        int pixels = DropPixels ();
        Debug.Log ("I drop " + pixels + " pixels!");

        CreateDestroyEffect ();
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

    public int DropPixels () {
        int Pixels = Random.Range (minPixelsDrop, maxPixelsDrop);
        PlayerManager.instance.player.GetComponent<Player> ().Pixels += Pixels;
        return Pixels;
    }
}
