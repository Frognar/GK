using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour {

    private List<Collider> inCollider = new List<Collider> ();
    [SerializeField] private List<string> tags = new List<string> ();

    private void Start () {
        inCollider.Clear ();
    }

    private void LateUpdate () {
        if (inCollider.Count > 0)
            GameManager.inBattle = true;

        else if (inCollider.Count == 0)
            GameManager.inBattle = false;
    }

    private void OnTriggerEnter (Collider other) {
        if (tags.Contains (other.tag))
            inCollider.Add (other);
    }

    private void OnTriggerExit (Collider other) {
        inCollider.Remove (other);
    }

}
