using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponStates { idle, firing, reloading };
public class WeaponManager : MonoBehaviour
{
    enum ControlTypes {Player, AI}
    [SerializeField] ControlTypes ControlType;
    [Space]
    public Weapon Weapon;
    public weaponStates weaponState;
    [Space]
    float fireTimer;

    void Update()
    {
        switch (ControlType)
        {
            case ControlTypes.Player:
                PlayerInput();
                PlayerTimers();
                break;
            case ControlTypes.AI:
                break;
            default:
                break;
        }
    }
    void FixedUpdate()
    {
        switch (ControlType)
        {
            case ControlTypes.Player:
                break;
            case ControlTypes.AI:
                break;
            default:
                break;
        }
    }

    void PlayerTimers()
    {
        switch (weaponState)
        {
            case weaponStates.idle:
                break;
            case weaponStates.firing:
                if (fireTimer > 0)
                    fireTimer -= Time.deltaTime;
                else
                    weaponState = weaponStates.idle;

                break;
            case weaponStates.reloading:
                break;
            default:
                break;
        }
    }

    void PlayerInput()
    {
        if (Weapon == null)
            return;

        switch (Weapon.FireType)
        {
            case FireTypes.SemiAutomatic:
                if (Input.GetMouseButtonDown(0))
                {
                    if (weaponState == weaponStates.idle)
                    {
                        weaponState = weaponStates.firing;
                        fireTimer = Weapon.fireTime;
                        print("The left mouse button was pressed");
                    }
                }
                break;
            case FireTypes.Automatic:
                if (Input.GetMouseButton(0))
                {
                    if (weaponState == weaponStates.idle)
                    {
                        weaponState = weaponStates.firing;
                        fireTimer = Weapon.fireTime;
                        print("The left mouse button is held down");
                    }
                }
                break;
            case FireTypes.BoltAction:
                break;
            default:
                break;
        }
    }
}
