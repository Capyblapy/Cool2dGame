using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public bool ChestHit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropChest()
    {
        if (ChestHit == true)
            return;

        ChestHit = true;

        DropItem(Random.Range(1, 3));
    }

    void DropItem(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            print("Loot " + i.ToString());
        }

        Destroy(gameObject);
    }
}
