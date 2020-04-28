using UnityEngine;

public class MouseInput : MonoBehaviour {
    [SerializeField] private float mouseSensitivity = 100f;
    private PlayerCamera playerCamera;
    private float nextTimeToFire = 0f;

    private IShoot weapon;
    private WeaponSwitching weaponSwitcher;

    private Player player;

    private void Awake () {
        playerCamera = GetComponentInChildren<PlayerCamera> ();
        weaponSwitcher = GetComponentInChildren<WeaponSwitching> ();
        player = GetComponent<Player> ();

        weapon = weaponSwitcher?.SelectWeapon ();
    }

    private void Update () {
        CameraMovement ();
        SwitchWaepon ();

        if (Input.GetButton ("Fire1") && Time.time >= nextTimeToFire)
            Shoot ();
    }

    private void CameraMovement () {
        playerCamera.YRotation = Input.GetAxis ("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerCamera.XRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    private void Shoot () {
        if (weapon != null) {
            if (player.UsePixels (weapon.PixelCost)) {
                weapon.Shoot ();
                nextTimeToFire = Time.time;
                nextTimeToFire += weapon.FireRate;
            }
        } else
            Debug.Log ("No weapon!");
    }

    private void SwitchWaepon () {
        if (weaponSwitcher != null) {
            if (Input.GetAxis ("Mouse ScrollWheel") > 0)
                weaponSwitcher.SelectedWeapon = 1;
            else if (Input.GetAxis ("Mouse ScrollWheel") < 0)
                weaponSwitcher.SelectedWeapon = -1;

            weapon = weaponSwitcher.SelectWeapon ();
        } else
            Debug.Log ("No weapon holder!");
    }
}
