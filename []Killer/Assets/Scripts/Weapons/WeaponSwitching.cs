using UnityEngine;
using System.Collections.Generic;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (!PauseManager.gameIsPaused && PlayerManager.instance.player.GetComponent<Player>().IsAlive)
        {
            HandleWeaponSwitching();
        }
    }

    void HandleWeaponSwitching()
    {
        int previousSelectedWeapon = selectedWeapon;

        // switch weapon using mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;

            else
                selectedWeapon++;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;

            else
                selectedWeapon--;
        }

        // switch weapon using keyboard numbers 1-3
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;

        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;

        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedWeapon = 2;

        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);

            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }
}
