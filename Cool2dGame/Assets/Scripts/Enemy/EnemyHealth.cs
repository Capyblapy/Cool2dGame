using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100f;
    public RoomGenerator rg;
    public GameObject Enemy;
    public GameObject EnemyList;
  
    void Start()
    {
        rg = FindObjectOfType<RoomGenerator>();
    }
    
    void FixedUpdate()
    {
        if (Health <= 0)
        {
            rg.EnemyList.Remove(Enemy);
            
            WeaponManager wm = gameObject.GetComponent<WeaponManager>();
            if (wm != null)
                Destroy(wm.BulletParent);

            Destroy(gameObject);
        }
    }
}
