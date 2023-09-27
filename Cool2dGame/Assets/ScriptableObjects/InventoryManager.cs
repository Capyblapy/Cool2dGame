using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> Inventory;
    public int Position = 0;

    [Header("UI")]
    public Image equiped;
    public Image upIcon;
    public Image downIcon;

    void Start()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            WeaponClass wClass = Inventory[i].GetComponent<WeaponClass>();
            if (wClass == null)
                Inventory.RemoveAt(i);
        }

        RefreshIcons();
    }

    void RefreshIcons()
    {
        equiped.sprite = Inventory[Position].GetComponent<WeaponClass>().icon;

        int upPos = Position-1;
        if(upPos==-1)
        {
            upPos = Inventory.Count;
        }
        upIcon.sprite = Inventory[upPos-1].GetComponent<WeaponClass>().icon;

        int downPos = Position + 1;
        if (downPos > Inventory.Count-1)
        {
            downPos = 0;
        }
        downIcon.sprite = Inventory[downPos].GetComponent<WeaponClass>().icon;
    }

    void Update()
    {
        float MouseSpin = Input.GetAxis("Mouse ScrollWheel");
        if (MouseSpin > 0) // Up Wheel
        {


            RefreshIcons();
        }
        else if(MouseSpin < 0) // Down Wheel
        {


            RefreshIcons();
        }
    }
}
