using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] TMP_Text CurrentAmmo;

    public WeaponManager wm;

    void FixedUpdate()
    {
        if (wm == null || wm.Weapon == null)
        {
            CurrentAmmo.text = "No weapon!";
            return;
        }

        Weapon weapon = wm.Weapon;

        if(wm.weaponState == weaponStates.reloading)
        {
            CurrentAmmo.text = "Reloading";
        }
        else
        {
            if (wm.currentAmmo <= 0)
                CurrentAmmo.text = "<color=red>"+wm.currentAmmo.ToString()+"</color>"+ "/"+weapon.Ammo.ToString();
            else
                CurrentAmmo.text = wm.currentAmmo.ToString() + "/" + weapon.Ammo.ToString();
        }
    }
}
