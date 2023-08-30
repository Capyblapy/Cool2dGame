using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponStates { idle, firing, reloading };
public enum ControlTypes { Player, AI }
public class WeaponManager : MonoBehaviour
{
    [SerializeField] ControlTypes ControlType;
    [Space]
    public Weapon Weapon;
    public weaponStates weaponState;
    [Space]
    float fireTimer;
    public AmmoPrefabs ammoPrefabs;

    void Update()
    {
        switch (ControlType)
        {
            case ControlTypes.Player:
                PlayerInput();
                break;
            case ControlTypes.AI:
                EnemyAI eAI = GetComponent<EnemyAI>();
                if (eAI == null)
                    break;

                if (eAI.Status == AiStatus.Shooting)
                    Shoot();

                break;
            default:
                break;
        }

        Timers();
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

    void Timers()
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
                    Shoot();
                }
                break;
            case FireTypes.Automatic:
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
                break;
            case FireTypes.BoltAction:
                break;
            default:
                break;
        }
    }

    void Shoot()
    {
        if(weaponState != weaponStates.firing)
        {
            weaponState = weaponStates.firing;
            fireTimer = Weapon.fireTime;
            switch (Weapon.AmmoType)
            {
                case AmmoTypes.Normal:
                    Projectile();
                    break;
                case AmmoTypes.Freezing:
                    break;
                default:
                    break;
            }
        }
    }

    void Projectile()
    {
        GameObject Bullet = Instantiate(ammoPrefabs.ammoPrefabs[0], transform.position + (transform.right * 1f), transform.rotation);
        Bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
        BulletScript BI = Bullet.GetComponent<BulletScript>();
        if (BI != null)
        {
            BI.ControlType = ControlType;
            BI.Weapon = Weapon;
        }
        else
            Destroy(Bullet);
    }

    void Raycast()
    {

    }
}
