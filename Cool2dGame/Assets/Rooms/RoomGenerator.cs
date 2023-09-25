using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int RoomSizeX;
    public int RoomSizeY;

    public GameObject blankTile;
    public GameObject wallTile;
    public GameObject floorTile;
    public GameObject enemySpawnTile;

    public GameObject[,] roomTiles;


    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GenerateRoom()
    {
        roomTiles = new GameObject[RoomSizeX, RoomSizeY];
        // Makes a for loop that activates the SpawnTile function inside the given coordinates
        for (int x = 0; x < RoomSizeX; x++)
        {
            for (int y = 0; y < RoomSizeY; y++)
            {
                SpawnTile(new Vector3(x, y, 0), new Vector2(x, y));
            }
        }

    }

    void SpawnTile(Vector3 spawnOffset, Vector2 index)
    {
        // if statement saying if the x or y coordinate is 0 or the max size of the room, spawn a wall, and if it isnt, spawn floor
        if(index.x == 0 || index.x == RoomSizeX-1 || index.y == 0 || index.y == RoomSizeY-1)
        {
            GameObject spawnedTile = Instantiate(wallTile);
            spawnedTile.transform.position = this.transform.position + spawnOffset;
        }
        else
        {
            GameObject spawnedTile = Instantiate(floorTile);
            spawnedTile.transform.position = this.transform.position + spawnOffset;
        }

        
    }


}
