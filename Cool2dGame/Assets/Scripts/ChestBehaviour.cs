using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public bool ChestHit = false;
    public LootList loot;

    public void DropChest()
    {
        if (ChestHit == true)
            return;

        ChestHit = true;

        DropItems(Random.Range(1, 3));
    }
    
    GameObject GetItem()
    {
        GameObject selectedLoot = loot.list[Random.Range(0, loot.list.Length)];
        if (selectedLoot != null && selectedLoot.GetComponent<WeaponClass>())
            return selectedLoot;

        return null;
    }

    void DropItems(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            print("Loot " + i.ToString());
            SpawnItem(GetItem());
        }

        Destroy(gameObject);
    }

    void SpawnItem(GameObject Item)
    {
        GameObject NewItem = Instantiate(Item);
        NewItem.transform.position = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * transform.position;
    }
}
