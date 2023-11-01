using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileTypes {floor, wall, hazard, chest, door};


public class TileScript : MonoBehaviour
{
    public tileTypes tileType;

    public SpriteRenderer tileRend;
    public BoxCollider2D tileCol;

    public Sprite[] tileSprites;

    List<GameObject> EnemyList;

    public void setTile(tileTypes newTileType)
    {
        
        tileType = newTileType;
        tileCol.enabled = true;

        switch (tileType)
        {
            case tileTypes.floor:
                tileCol.enabled = false;
                tileRend.sprite = tileSprites[0];
                break;
            case tileTypes.wall:
                tileRend.sprite = tileSprites[1];
                break;
            case tileTypes.hazard:
                break;
            case tileTypes.chest:
                break;
            case tileTypes.door:
                tileRend.sprite = tileSprites[0];
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
        if(tileType == tileTypes.door)
        {
            EnemyList = transform.parent.GetComponent<RoomGenerator>().EnemyList;
            if(EnemyList.Count == 0){
                tileCol.enabled = false;
            } 
        }
    }
}
