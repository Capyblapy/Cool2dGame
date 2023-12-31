using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    public bool isBoss;

    public int RoomLocation = 1;

    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        roomList.Add(this.gameObject);
    }

    void Start()
    {
        //        start     offset            oD       nD                nX                     nY
        summonRoom(true, new Vector3(0, 0, 0), 0, Random.Range(1, 5), Random.Range(10, 31), Random.Range(10, 31));
    }

    public void generateRoom()
    {
        RoomGenerator rg = roomList.Last().GetComponent<RoomGenerator>();
        // Set up
        Vector3 newOffset = new Vector3(0, 0);
        int newX = Random.Range(10, 41);
        int newY = Random.Range(10, 41);
        if (roomList.Count == 10)
            isBoss = true;
        if(isBoss == true)
        {
            newX = 40;
            newY = 30;
        }
        // Figures out new door & Offset for each door
        int openDoor = 0;
        switch (rg.unlockDoor)
        {
            case 1: // Left
                openDoor = 3;
                newOffset = new Vector3((newX * -1) + 1, (rg.RoomSizeY / 2) - (newY / 2));
                break;
            case 2: // Bottom
                openDoor = 4;
                newOffset = new Vector3((rg.RoomSizeX / 2) - (newX / 2), (newY * -1) + 1);
                break;
            case 3: // Right
                openDoor = 1;
                newOffset = new Vector3(rg.RoomSizeX - 1, (rg.RoomSizeY / 2) - (newY / 2));
                break;
            case 4: // Top
                openDoor = 2;
                newOffset = new Vector3((rg.RoomSizeX / 2) - (newX / 2), rg.RoomSizeY - 1);
                break;
        }

        int unlockDoor = 0;
        unlockDoor = Random.Range(1, 5);
        while (unlockDoor == openDoor)
        {
            unlockDoor = Random.Range(1, 5);
            
        }
        
        if(isBoss == true)
        {
            
            summonRoom(false, newOffset, openDoor, 0, newX, newY);
        }
        else
        {
            summonRoom(false, newOffset, openDoor, unlockDoor, newX, newY);
        }
        
    }
   
    void summonRoom(bool newBool, Vector3 offsetVector, int openDoor,int unlockDoor, int rsX, int rsY)
    {
        GameObject room = Instantiate(roomPrefab);
        print(roomList.Last().transform.position);
        print(offsetVector);
        print(roomList.Last().transform.position + offsetVector);
        room.transform.position = roomList.Last().transform.position + offsetVector;
        room.transform.SetParent(this.transform, false);

        RoomGenerator roomGen = room.GetComponent<RoomGenerator>();
        roomGen.firstRoom = newBool;
        roomGen.openDoor = openDoor;
        roomGen.unlockDoor = unlockDoor;
        roomGen.RoomSizeX = rsX;
        roomGen.RoomSizeY = rsY;
        roomGen.GenerateRoom();

        roomList.Add(room);
    }

    public void PlayerTrigger(GameObject room)
    {
        if (room == null) return;
        print(room);

        int roomIndex = roomList.IndexOf(room);
        GameObject pRoom = roomList[roomIndex - 1];
        GameObject fRoom;
        
        if (roomIndex == roomList.Count - 1)
            fRoom = room;
        else
            fRoom = roomList[roomIndex + 1];

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i] == pRoom || roomList[i] == fRoom || roomList[i] == room || roomList[i] == this.gameObject)
                roomList[i].SetActive(true);
            else
                roomList[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && roomList.Count != 11)
        {
            generateRoom();
            Player.transform.position = new Vector3(roomList[roomList.Count - 1].GetComponent<RoomGenerator>().RoomSizeX / 2, roomList[roomList.Count - 1].GetComponent<RoomGenerator>().RoomSizeY / 2);
       }
    }
}
