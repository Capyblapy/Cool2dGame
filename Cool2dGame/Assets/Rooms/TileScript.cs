using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileTypes {floor, wall, hazard, chest};


public class TileScript : MonoBehaviour
{
    public tileTypes tileType;

    public SpriteRenderer tileRend;
    public BoxCollider2D tileCol;

    public Sprite[] tileSprites;

    public void setTile(tileTypes newTileType)
    {
        tileType = newTileType;

        switch (tileType)
        {
            case tileTypes.floor:
                tileCol.enabled = false;
                tileRend.sprite = tileSprites[0];
                break;
            case tileTypes.wall:
                tileCol.enabled = true;
                tileRend.sprite = tileSprites[1];
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
