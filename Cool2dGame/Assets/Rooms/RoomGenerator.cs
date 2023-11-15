using System.Collections;
using System.Collections.Generic;
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
    int ChosenDoor;
    public bool firstRoom = false;
    public bool roomGenerated = false;

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
                SpawnLoot(LootChest);
                hasLootSpawned = true;
            }
        }
    }

    public void GenerateRoom()
    {
        RoomSizeX = Random.Range(10, 41);
        RoomSizeY = Random.Range(10,41);
        ChosenDoor = Random.Range(1,5); // Random.Range is exclusive for ints, Need to do 1,5 for a random number of 1 to 4
        print(ChosenDoor);
        roomTiles = new GameObject[RoomSizeX, RoomSizeY];
        // Makes a for loop that activates the SpawnTile function inside the given coordinates
        for (int x = 0; x < RoomSizeX; x++)
        {
            for (int y = 0; y < RoomSizeY; y++)
            {
                SpawnTile(new Vector3(x, y, 0), new Vector2(x, y));
            }
        }

        roomGenerated = true;
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
            switch (ChosenDoor)
            {
                case 1:
                    if(index.x == 0 && index.y == RoomSizeY/2)
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door); 

                    break;
                
                case 2:
                    if(index.x == RoomSizeX/2 && index.y == 0)
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door); 

                    break;

                case 3:
                    if(index.x == RoomSizeX-1 && index.y == RoomSizeY/2)
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);

                    break;

                case 4:
                    if(index.x == RoomSizeX/2 && index.y == RoomSizeY-1)
                        spawnedTile.GetComponent<TileScript>().setTile(tileTypes.door);

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

        if (spawnedTile == roomTiles[RoomSizeX/2, RoomSizeY/2] && firstRoom == true)
        {
            print("Centre tile!");
            spawnedTile.GetComponent<TileScript>().setTile(tileTypes.floor);
            // spawn player code here wow :o
            Player.transform.position = spawnedTile.transform.position;

            firstRoom = false;
        }
        
    }

    void MakeIsland(GameObject spawnedTile)
    {
        float rng = Random.Range(0, 100);
        if(rng <= islandFrequency)
        {
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
            
        }
    }

    void SpawnLoot(GameObject LootChest)
    {
        Instantiate(LootChest);
    }

}
