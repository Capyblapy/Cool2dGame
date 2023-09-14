using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int RoomSizeX;
    public int RoomSizeY;

    public GameObject blankTile;


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

        for (int x = 0; x < RoomSizeX; x++)
        {
            for (int y = 0; y < RoomSizeY; y++)
            {
                SpawnTile(new Vector3(x, y, 0));
            }
        }

    }

    void SpawnTile(Vector3 spawnOffset)
    {
        GameObject spawnedTile = Instantiate(blankTile);

        spawnedTile.transform.position = this.transform.position + spawnOffset;
    }


}
