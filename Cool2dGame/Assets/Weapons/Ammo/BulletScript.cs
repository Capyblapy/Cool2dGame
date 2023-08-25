using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public ControlTypes ControlType;
    public Weapon Weapon;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        print(Weapon == null);
        if (Weapon == null)
            return;

        switch (Weapon.AmmoType)
        {
            case AmmoTypes.Normal:
                Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 mouseScreenPosition = Input.mousePosition;

                Vector3 playerToMouseVector = (mouseScreenPosition - playerScreenPosition).normalized;

                rigidbody2d.velocity = playerToMouseVector * 50;
                break;
            case AmmoTypes.Freezing:
                break;
            default:
                break;
        }
    }
}
