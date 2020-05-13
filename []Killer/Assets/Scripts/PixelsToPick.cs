using UnityEngine;


/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class PixelsToPick : MonoBehaviour
{
    private int pixels;

    public void SetPixels (int pixels) {
        this.pixels = pixels;
    }

    private void Start () {
        CheckHeight ();
    }

    private void CheckHeight () {
        if (Physics.Raycast (transform.position, -transform.up, out RaycastHit hit)) {
            if (hit.collider.gameObject.layer == 8) // Ground
                transform.position = hit.point + new Vector3 (0f, 0.5f, 0f);
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            Player player = other.GetComponent<Player> ();
            if (player != null) {
                player.Pixels += pixels;
                Destroy (gameObject);
            }
        }
    }
}
