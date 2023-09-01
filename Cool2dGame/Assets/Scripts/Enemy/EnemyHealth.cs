using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100f;

    void FixedUpdate()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }
}
