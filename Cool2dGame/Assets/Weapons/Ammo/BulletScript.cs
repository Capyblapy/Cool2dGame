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

    private void LateUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(OldPos, transform.position - OldPos);
        OldPos = transform.position;

        if (hit.collider != null && hit.collider != GetComponent<BoxCollider2D>())
            OnTriggerEnter2D(hit.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);

        if(col.gameObject.GetComponent<PlayerHealth>())
        {

        }
        else if(col.gameObject.GetComponent<EnemyAI>())
        {

        }
    }

    private void Move()
    {
        rigidbody2d.velocity = transform.up * 50;
    }
}

