using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class WeaponSwitching : MonoBehaviour
{
    private int selectedWeapon = 0;
    public int SelectedWeapon {
        get {
            return selectedWeapon;
        }
        set {
            selectedWeapon -= value;
            if (selectedWeapon > transform.childCount - 1)
                selectedWeapon = 0;
            else if (selectedWeapon < 0)
                selectedWeapon = transform.childCount - 1;
        }
    }

    public IShoot SelectWeapon () {
        IShoot wea = null;
        int i = 0;
        foreach (Transform weapon in transform) {
            if (i == selectedWeapon) {
                weapon.gameObject.SetActive (true);
                wea = weapon.GetComponent<IShoot> ();
            } else
                weapon.gameObject.SetActive (false);
            i++;
        }
        return wea;
    }
}
