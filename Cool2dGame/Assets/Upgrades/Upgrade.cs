using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrade", order = 2)]
public class Upgrade : ScriptableObject
{
    [Header("Info")]
    public UpgradeTypes Type;
    public string Name;

    [Header("Requirments")]
    public Requirments[] RequiredMaterials;
    public Upgrade[] RequiredUpgrades;
}