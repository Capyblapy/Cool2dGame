using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rooms", menuName = "ScriptableObjects/RoomPrefabs", order = 5)]
public class RoomPrefabs : ScriptableObject
{
    public GameObject[] roomPrefabs;
}
