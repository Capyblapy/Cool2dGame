using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileTypes {floor, wall, hazard, chest};

public class TileScript : MonoBehaviour
{
    public tileTypes tileType;

    
    

    public void setTile(tileTypes newTileType)
    {
        tileType = newTileType;

        switch (tileType)
        {
            case tileTypes.floor:
                break;
            case tileTypes.wall:
                break;
            case tileTypes.hazard:
                break;
            case tileTypes.chest:
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
