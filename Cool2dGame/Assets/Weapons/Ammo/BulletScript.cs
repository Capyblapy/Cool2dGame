using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public ControlTypes ControlType;
    public Weapon Weapon;

    public ParticalPrefabs particalPrefab;

    public float TimeAlive = 100;

    Vector3 OldPos;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        OldPos = transform.position;
    }

    private void Start()
    {
        // Spread Here
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
        RaycastHit2D hit = Physics2D.Raycast(OldPos, transform.position - OldPos, 0.8f);
        OldPos = transform.position;

        if (hit.collider != null && hit.collider != GetComponent<BoxCollider2D>())
            OnTriggerEnter2D(hit.collider);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.DrawLine(OldPos, transform.position, Color.red, Weapon.fireTime - 0.01f)
        if (CompareTag(col.gameObject.tag) == true)
            return;

        if(col.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth PH = col.gameObject.GetComponent<PlayerHealth>();
            PH.Health -= Weapon.Damage;

            SpawnPartical(0, col.gameObject);
        }

        print(col.gameObject.GetComponent<EnemyHealth>());
        if (col.gameObject.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth EH = col.gameObject.GetComponent<EnemyHealth>();
            EH.Health -= Weapon.Damage;

            SpawnPartical(0, col.gameObject);
        }

        Destroy(gameObject);
    }

    private void Move()
    {
        rigidbody2d.velocity = transform.up * 50;
    }

    private void SpawnPartical(int ParticalNum, GameObject col)
    {
        GameObject partical = Instantiate(particalPrefab.particalPrefabs[ParticalNum], col.transform);
        partical.transform.parent = col.transform;
    }
}

