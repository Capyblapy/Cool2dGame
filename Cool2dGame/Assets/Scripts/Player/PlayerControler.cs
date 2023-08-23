using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum playerStates {idle, walking, spriting, dashing};

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    public playerStates playerState;
    Rigidbody2D rigidbody2d;

    [Header("General Stuff")]
    [SerializeField] float Stamina = 100f;

    [Header("WASD Movment")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float dragAmount = 10f;

    [Header("Dash")]
    [SerializeField] string DashKey = "space";
    [SerializeField] float DashTime = 1f;
    float DashTimer;
    [SerializeField] float DashSpeed = 15f;
    [SerializeField] float dashMaxSpeed = 75;
    [SerializeField] bool canDash = true;

    [Header("Running")]
    [SerializeField] string SprintKey = "left shift";
    [SerializeField] int SprintStam = 10;
    [SerializeField] float SprintSpeed = 10f;
    [SerializeField] float sprintMaxSpeed = 50;
    [SerializeField] bool Sprinting = false;

    Vector2 motionVector;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
        Timers();
    }

    void FixedUpdate()
    {
        if (Sprinting == false && canDash == true)
        {
            if (Stamina + 0.05f <= 100f)
                Stamina += 0.05f;
        }

        Move();
    }

    void PlayerInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector3(
            horizontal,
            vertical,
            0.00f
        );

        if (Input.GetKeyDown(DashKey))
            Dash();

        if (Input.GetKeyDown(SprintKey))
            Sprinting = !Sprinting;
    }

    void Timers()
    {
        switch (playerState)
        {
            case playerStates.idle:
                break;
            case playerStates.walking:
                break;
            case playerStates.spriting:
                break;
            case playerStates.dashing:

                if (DashTimer > 0)
                    DashTimer -= Time.deltaTime;
                else
                    playerState = playerStates.idle;

                break;
            default:
                break;
        }
    }

    private void Move()
    {
        if (Sprinting == false || Stamina - 0.05f <= 0)
        {
            Sprinting = false;
            rigidbody2d.velocity += motionVector * speed;
        }
        else if (Sprinting == true)
        {
            rigidbody2d.velocity += motionVector * SprintSpeed;
            Stamina -= 0.05f;
        }

        print(rigidbody2d.velocity);
        switch (playerState)
        {
            case playerStates.idle:
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);
                break;
            case playerStates.walking:
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);
                break;
            case playerStates.spriting:
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, sprintMaxSpeed);
                break;
            case playerStates.dashing:
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, dashMaxSpeed);
                break;
            default:
                break;
        }
        //rigidbody2d.velocity =  Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);


        if(motionVector == new Vector2(0.00f,0.00f))
            rigidbody2d.velocity = Vector3.MoveTowards(rigidbody2d.velocity, Vector3.zero, dragAmount * Time.deltaTime);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Dash()
    {
        if (Stamina < 25f || canDash == false)
            return;

        Stamina -= 25f;
        canDash = false;

        playerState = playerStates.dashing;
        DashTimer = DashTime;

        // Dash Code Here

        canDash = true;
    }
}