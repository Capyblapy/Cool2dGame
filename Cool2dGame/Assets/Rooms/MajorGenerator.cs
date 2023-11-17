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
        summonRoom(true, new Vector3(0, 0, 0), Random.Range(1, 5), Random.Range(10, 31), Random.Range(10, 31));
    }

    public void generateRoom(GameObject originRoom)
    {
        RoomGenerator rg = originRoom.GetComponent<RoomGenerator>();
        // Set up
        Vector3 newOffset = new Vector3(0,0);
        int newX = Random.Range(10, 41);
        int newY = Random.Range(10, 41);


        // Figures out new door & Offset for each door
        int newDoor = 0;
        switch (rg.ChosenDoor)
        {
            case 1: // Left
                newDoor = 3;
                newOffset = new Vector3((newX*-1)+1,(rg.RoomSizeY/2)-(newY/2));
                break;
            case 2: // Bottom
                newDoor = 4;
                newOffset = new Vector3((rg.RoomSizeX / 2) - (newX / 2), (newY*-1)+1);
                break;
            case 3: // Right
                newDoor = 1;
                newOffset = new Vector3(rg.RoomSizeX - 1, (rg.RoomSizeY / 2) - (newY / 2));
                break;
            case 4: // Top
                newDoor = 2;
                newOffset = new Vector3((rg.RoomSizeX/2)-(newX/2), rg.RoomSizeY-1);
                break;
        }

        summonRoom(false, newOffset, newDoor, newX, newY);
    }

    void summonRoom(bool newBool, Vector3 offsetVector, int Door, int rsX, int rsY)
    {
        GameObject room = Instantiate(roomPrefab);
        room.transform.position = transform.position + offsetVector;
        room.transform.SetParent(this.transform, false);

        RoomGenerator roomGen = room.GetComponent<RoomGenerator>();
        roomGen.firstRoom = newBool;
        roomGen.ChosenDoor = Door;
        roomGen.RoomSizeX = rsX;
        roomGen.RoomSizeY = rsY;
        roomGen.GenerateRoom();

        roomList.Add(room);
    }
}
