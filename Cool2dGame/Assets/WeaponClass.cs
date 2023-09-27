using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public Weapon weapon;
    public Sprite icon;
    public int currentAmmo;

    private void Start()
    {
        currentAmmo = weapon.Ammo;
    }
}
