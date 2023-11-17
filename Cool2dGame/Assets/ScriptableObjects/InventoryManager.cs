using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Managment")]
    public List<GameObject> Inventory;
    public int Position;

    [Header("UI")]
    public Image equiped;
    public Image upIcon;
    public Image downIcon;

    void Start()
    {
        Position = 0;

        for (int i = 0; i < Inventory.Count; i++)
        {
            WeaponClass wClass = Inventory[i].GetComponent<WeaponClass>();
            if (wClass == null)
                Inventory.RemoveAt(i);
        }

        RefreshInventory();
    }

    void SwapItem(bool Status) // true up, false down
    {
        WeaponClass oWc = Inventory[Position].GetComponent<WeaponClass>();

        // Selected
        int newPos;
        if (Status == true)
        {
            newPos = Position - 1;
            if (newPos == -1)
            {
                newPos = Inventory.Count - 1;
            }
        }
        else
        {
            newPos = Position + 1;
            if (newPos > Inventory.Count-1)
            {
                newPos = 0;
            }
        }

        Position = newPos;

        // Weapon Swapping
        WeaponClass wc = Inventory[Position].GetComponent<WeaponClass>();
        WeaponManager wm = gameObject.GetComponent<WeaponManager>();
        if (wm == null)
            return;

        oWc.currentAmmo = wm.currentAmmo;
        wm.EquipWeapon(wc.weapon, wc.currentAmmo);

        RefreshInventory();
    }

    void RefreshInventory()
    {
        if(Inventory.Count == 0)
        {
            upIcon.sprite = null;
            downIcon.sprite = null;
            equiped.sprite = null;

            return;
        }

        int upPos = Position - 1;
        if (upPos == -1)
        {
            upPos = Inventory.Count-1;
        }

        int downPos = Position + 1;
        if (downPos > Inventory.Count - 1)
        {
            downPos = 0;
        }

        upIcon.sprite = Inventory[upPos].GetComponent<WeaponClass>().icon;
        downIcon.sprite = Inventory[downPos].GetComponent<WeaponClass>().icon;
        equiped.sprite = Inventory[Position].GetComponent<WeaponClass>().icon;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Loot")
            return;

        WeaponClass wc = col.gameObject.GetComponent<WeaponClass>();
        if (wc == null)
            return;

        Inventory.Add(col.gameObject);

        col.gameObject.SetActive(false);

        SwapItem(true);
        SwapItem(false);
    }

    void Update()
    {
        float MouseSpin = Input.GetAxis("Mouse ScrollWheel");
        if (MouseSpin > 0) // Up Wheel
            SwapItem(true);
        else if(MouseSpin < 0) // Down Wheel
            SwapItem(false);

        if (Input.GetKeyDown(KeyCode.Q))
            SwapItem(true);
        if (Input.GetKeyDown(KeyCode.E))
            SwapItem(false);
    }
}
