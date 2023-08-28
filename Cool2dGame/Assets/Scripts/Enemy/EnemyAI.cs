using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStatus { Idle, Moving, Shooting }

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    GameObject Player;
    public AiStatus Status;
    public float Health = 100f;
    [SerializeField] float EnemySpeed;
    Vector3 dirToPlayer;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Player = PlayerControler.Player;
        Status = AiStatus.Moving;
    }

    private void FixedUpdate()
    {
        homingFunction();
    }

    public void homingFunction()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= 5)
            Status = AiStatus.Shooting;
        else
            Status = AiStatus.Moving;

        switch (Status)
        {
            case AiStatus.Idle:
                break;
            case AiStatus.Moving:
                dirToPlayer = (Player.transform.position - transform.position).normalized;
                rigidbody2d.velocity = dirToPlayer * EnemySpeed;
                break;
            case AiStatus.Shooting:
                rigidbody2d.velocity = new Vector2(0,0);
                break;
            default:
                break;
        }

        // Look Movment, Dont Effect by shooting.
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
