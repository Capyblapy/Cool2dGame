using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum tileTypes {floor, wall, hazard, chest, door};


public class TileScript : MonoBehaviour
{
    public tileTypes tileType;

    public SpriteRenderer tileRend;
    public BoxCollider2D tileCol;

    public Sprite[] tileSprites;

    List<GameObject> EnemyList;
    bool won = false;

    public void setTile(tileTypes newTileType)
    {
        
        tileType = newTileType;
        tileCol.enabled = true;

        switch (tileType)
        {
            case tileTypes.floor:
                tileCol.enabled = false;
                tileRend.sprite = tileSprites[0];
                this.name = "Floor";
                break;
            case tileTypes.wall:
                tileRend.sprite = tileSprites[1];
                this.name = "Wall";
                break;
            case tileTypes.hazard:
                break;
            case tileTypes.chest:
                break;
            case tileTypes.door:
                tileRend.sprite = tileSprites[0];
                this.name = "Door";
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
        if (tileType == tileTypes.door && won == false)
        {
            EnemyList = transform.parent.GetComponent<RoomGenerator>().EnemyList;
            if(EnemyList.Count == 0){
                Action();
                won = true;
            } 
        }
    }

    public void Action()
    {
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
            case tileTypes.door:
                // sprite change stuff here
                tileCol.enabled = false;
                break;
        }
    }
}
