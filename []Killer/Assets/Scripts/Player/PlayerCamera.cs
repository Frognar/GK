using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    private Transform playerBody;
    private float xRotation = 0f;
    private float yRotation;

    public float XRotation {
        get {
            return xRotation;
        }
        set {
            xRotation = Mathf.Clamp (value, -90f, 90f);
        }
    }

    public float YRotation {
        set {
            yRotation = value;
        }
    }

    private void Awake () {
        playerBody = transform.parent;
    }

    private void Update () {
        this.transform.localRotation = Quaternion.Euler (xRotation, 0f, 0f);
        playerBody.Rotate (Vector3.up * yRotation);
    }
}
