using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int RoomSizeX;
    public int RoomSizeY;
    public int islandFrequency;
    public float EnemySpawnRate;

    GameObject Player;
    GameObject Tile;
    GameObject Enemy;
    GameObject LootChest;

    public GameObject[,] roomTiles;

    public List<GameObject> EnemyList;

    public bool hasLootSpawned = false;
    public int unlockDoor;
    public int openDoor;
    public bool firstRoom = false;
    public bool roomGenerated = false;

    BoxCollider2D roomTrigger;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = MajorGenerator.Instance.Player;
        Tile = MajorGenerator.Instance.Tile;
        Enemy = MajorGenerator.Instance.Enemy;
        LootChest = MajorGenerator.Instance.LootChest;
    }


    // Update is called once per frame
    void Update()
    {
        if (roomGenerated == false)
            return;

        if(EnemyList.Count == 0){
            if(hasLootSpawned == false){
                roomWin();
                hasLootSpawned = true;
            }
        }
    }

    public void GenerateRoom()
    {
        print(unlockDoor);
        roomTiles = new GameObject[RoomSizeX, RoomSizeY];
        // Makes a for loop that activates the SpawnTile function inside the given coordinates
        for (int x = 0; x < RoomSizeX; x++)
        {
            for (int y = 0; y < RoomSizeY; y++)
            {
                SpawnTile(new Vector3(x, y, 0), new Vector2(x, y));
            }
        }
        roomTiles[1, RoomSizeY / 2].GetComponent<TileScript>().setTile(tileTypes.floor);
               
        roomTiles[RoomSizeX / 2, 1].GetComponent<TileScript>().setTile(tileTypes.floor);
              
        roomTiles[RoomSizeX - 2, RoomSizeY / 2].GetComponent<TileScript>().setTile(tileTypes.floor);
               
        roomTiles[RoomSizeX / 2, RoomSizeY - 2].GetComponent<TileScript>().setTile(tileTypes.floor);

        
        roomTrigger = this.AddComponent<BoxCollider2D>();
        roomTrigger.isTrigger = true;
        roomTrigger.offset = new Vector2(RoomSizeX / 2, RoomSizeY / 2);
        roomTrigger.size = new Vector2(RoomSizeX - 2, RoomSizeY - 2);

        roomGenerated = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            MajorGenerator.Instance.PlayerTrigger(this.gameObject);
        }
    }

    void SpawnTile(Vector3 spawnOffset, Vector2 index)
    {
        GameObject spawnedTile = Instantiate(Tile);
        spawnedTile.transform.SetParent(this.transform, false);

        // if statement saying if the x or y coordinate is 0 or the max size of the room, spawn a wall, and if it isnt, spawn floor
        if(index.x == 0 || index.x == RoomSizeX-1 || index.y == 0 || index.y == RoomSizeY-1)
        {
            spawnedTile.GetComponent<TileScript>().setTile(tileTypes.wall);
            spawnedTile.transform.position = this.transform.position + spawnOffset;
            
            roomTiles[(int)index.x, (int)index.y] = spawnedTile;
            switch (unlockDoor)
            {
                case 1:
                    if(index.x == 0 && index.y == RoomSizeY/2) { // left
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door); 
                       // roomTiles[1,RoomSizeY/2].GetComponent<TileScript>().setTile(tileTypes.floor);
                    }
                    //if(index.x == 1 && index.y == RoomSizeY/2)
                    //    spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
                    break;
                
                case 2:
                    if(index.x == RoomSizeX/2 && index.y == 0) {// bottom
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door); 
                       // roomTiles[RoomSizeX/2, 1].GetComponent<TileScript>().setTile(tileTypes.floor);
                    }
                    //if (index.x == RoomSizeY/2 && index.y == 0)
                    //    spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
                    break;

                case 3:
                    if(index.x == RoomSizeX-1 && index.y == RoomSizeY/2){ // right
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        //roomTiles[RoomSizeX-2, RoomSizeY/2].GetComponent<TileScript>().setTile(tileTypes.floor);
                    }
                    //if (index.x == RoomSizeX-2 && index.y == RoomSizeY/2)
                    //    spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
                    break;

                case 4:
                    if(index.x == RoomSizeX/2 && index.y == RoomSizeY-1) {// top
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        //roomTiles[RoomSizeX/2,RoomSizeY-2].GetComponent<TileScript>().setTile(tileTypes.floor);
                    }
                   // if (index.x == RoomSizeX/2 && index.y == RoomSizeY-2)
                     //   spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
                    break;
            }

            switch (openDoor)
            {
                case 0:
                    break;

                case 1:
                    if (index.x == 0 && index.y == RoomSizeY / 2) // left
                    {
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        spawnedTile.GetComponent<TileScript>().Action();
                    }

                    break;

                case 2:
                    if (index.x == RoomSizeX / 2 && index.y == 0) // bottom
                    {
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        spawnedTile.GetComponent<TileScript>().Action();
                    }

                    break;

                case 3:
                    if (index.x == RoomSizeX - 1 && index.y == RoomSizeY / 2) // right
                    {
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        spawnedTile.GetComponent<TileScript>().Action();
                    }

                    break;

                case 4:
                    if (index.x == RoomSizeX / 2 && index.y == RoomSizeY - 1) // top
                    {
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);
                        spawnedTile.GetComponent<TileScript>().Action();
                    }

                    break;
            }
        }
        
        else
        {
            spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
            spawnedTile.transform.position = this.transform.position + spawnOffset;
            
            roomTiles[(int)index.x, (int)index.y] = spawnedTile;
            
            SpawnEnemy(Enemy, spawnedTile.transform.position);

            MakeIsland(spawnedTile);
        }


        if (firstRoom == true)
        {
            if (spawnedTile == roomTiles[RoomSizeX / 2, RoomSizeY / 2])
            {
                print("Centre tile!");
                spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
                // spawn player code here wow :o
                Player.transform.position = spawnedTile.transform.position;
            }

            firstRoom = false;
        }
        
    }

    void MakeIsland(GameObject spawnedTile)
    {
        float rng = Random.Range(0, 100);
        if(rng <= islandFrequency)
        {
            if (MajorGenerator.Instance.isBoss == true)
                spawnedTile.GetComponent<TileScript>().setTile(tileTypes.hazard);
            else
                spawnedTile.GetComponent<TileScript>().setTile(tileTypes.wall);
            
        }
    }

    void SpawnEnemy(GameObject Enemy, Vector3 position)
    {
        float rng = Random.Range(1, 100);
        if(rng <= EnemySpawnRate)
        {

            GameObject EnemyInScene = Instantiate(Enemy);
            EnemyInScene.transform.position = position;
            EnemyList.Add(EnemyInScene);
            Enemy.transform.SetParent(this.transform, false);
            
        }
    }

    void roomWin()
    {
        // Loot Chest
        GameObject newLoot = Instantiate(LootChest);
        newLoot.transform.position = roomTiles[RoomSizeX / 2, RoomSizeY / 2].transform.position;

        // New Room
        if (MajorGenerator.Instance.isBoss == true)
            return;
        MajorGenerator.Instance.generateRoom();
        
    }
}