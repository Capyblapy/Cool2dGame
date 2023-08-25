using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public float EnemySpeed;
    public Vector3 dirToPlayer;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemySpeed = 3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        homingFunction();
    }

    public void homingFunction()
    {
        dirToPlayer = (Player.transform.position - transform.position).normalized;
        rigidbody2d.velocity = dirToPlayer * EnemySpeed;

        Vector2 direction = Player.transform.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
