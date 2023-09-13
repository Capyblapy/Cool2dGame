using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] TMP_Text MaxAmmo;
    [SerializeField] TMP_Text CurrentAmmo;

    public WeaponManager wm;

    void FixedUpdate()
    {
        if (wm == null || wm.Weapon == null)
            return;

        Weapon weapon = wm.Weapon;

        MaxAmmo.text = "/"+weapon.Ammo.ToString();
        CurrentAmmo.text = wm.currentAmmo.ToString();

        if (wm.currentAmmo <= 5)
            CurrentAmmo.color = Color.red;
        else
            CurrentAmmo.color = Color.white;
    }
}
