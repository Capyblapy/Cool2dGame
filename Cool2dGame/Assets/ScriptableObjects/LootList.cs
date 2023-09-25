using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LootList", order = 4)]
public class LootList : ScriptableObject
{
    public GameObject[] list;
}