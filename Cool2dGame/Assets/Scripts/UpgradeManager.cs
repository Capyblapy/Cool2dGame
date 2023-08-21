using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpgradeManager : MonoBehaviour
{

}

// Enums
public enum UpgradeTypes{Test1, Test2, Test3, Test4};
public enum Materials{Test1, Test2, Test3, Test4};

// Classes
[Serializable]
public class Requirments
{
    public Materials Material;
    public int Ammount;
}