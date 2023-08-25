using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireTypes {SemiAutomatic, Automatic, BoltAction}
public enum AmmoTypes {Normal }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    [Header("General Info")]
    public string Name;
    public FireTypes FireType;
    public AmmoTypes AmmoType;

    [Header("Ammo Info")]
    public float Ammo;
    public float ReloadTime;

    [Header("Shoot Info")]
    public float Spread;
    public float Damage;
    public float BulletAmmount;
    public float fireTime;

}
