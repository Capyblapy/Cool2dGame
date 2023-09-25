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

    void NewPosition(GameObject Item, GameObject NewItem)
    {
        NewItem.transform.position = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
    }

    void SpawnItem(GameObject Item)
    {
        GameObject NewItem = Instantiate(Item);
        NewPosition(Item, NewItem);

        RaycastHit2D hit = Physics2D.Raycast(NewItem.transform.position, transform.position - NewItem.transform.position, 0.8f);
        if (hit.collider != null && hit.collider != GetComponent<BoxCollider2D>())
            NewPosition(Item, NewItem);
    }
}
