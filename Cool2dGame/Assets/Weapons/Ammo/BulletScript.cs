using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public ControlTypes ControlType;
    public Weapon Weapon;

    public float TimeAlive = 100;

    Vector3 OldPos;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        OldPos = transform.position;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, OldPos, Color.red);
        OldPos = transform.position;

        if (TimeAlive - Time.deltaTime <= 0)
            Destroy(gameObject);
        else
            TimeAlive -= Time.deltaTime;

        if (Weapon == null)
            return;

        switch (Weapon.AmmoType)
        {
            case AmmoTypes.Normal:
                Move();
                break;
            case AmmoTypes.Freezing:
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        rigidbody2d.velocity = transform.up * 50;
    }
}

