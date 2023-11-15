using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorGenerator : MonoBehaviour
{
    public static MajorGenerator Instance;

    public GameObject roomPrefab;
    public List<GameObject> roomList;

    public GameObject Player;
    public GameObject Tile;
    public GameObject Enemy;
    public GameObject LootChest;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObject newRoom = Instantiate(roomPrefab);
        newRoom.transform.position = transform.position;
        newRoom.transform.SetParent(this.transform, false);

        RoomGenerator roomGen = newRoom.GetComponent<RoomGenerator>();
        roomGen.firstRoom = true;
        roomGen.GenerateRoom();

        roomList.Add(newRoom);
    }

    public void newRoom()
    {
        GameObject newRoom = Instantiate(roomPrefab);
        newRoom.transform.position = transform.position;
        newRoom.transform.SetParent(this.transform, false);

        RoomGenerator roomGen = newRoom.GetComponent<RoomGenerator>();
        roomGen.GenerateRoom();

        roomList.Add(newRoom);
    }
}
