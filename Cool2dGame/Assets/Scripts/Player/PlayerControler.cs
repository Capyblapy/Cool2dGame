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
    [SerializeField] float RegenStam = 0.05f;

    [Header("WASD Movment")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float dragAmount = 100f;

    [Header("Dash")]
    [SerializeField] string DashKey = "space";
    [SerializeField] float DashTime = 0.2f;
    float DashTimer;
    [SerializeField] float DashSpeed = 30f;
    [SerializeField] float dashMaxSpeed = 30f;

    [Header("Running")]
    [SerializeField] string SprintKey = "left shift";
    [SerializeField] float SprintStam = 0.05f;
    [SerializeField] float SprintSpeed = 10f;
    [SerializeField] float sprintMaxSpeed = 10f;

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

        if (Input.GetKey(SprintKey))
            Sprint(true);
        else
            Sprint(false);
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
                    playerState = playerStates.walking;

                break;
            default:
                break;
        }
    }

    private void Move()
    {
        if (playerState == playerStates.spriting && (Stamina - 0.05f <= 0) == false)
           playerState = playerStates.walking;

        switch (playerState)
        {
            case playerStates.idle:
                rigidbody2d.velocity += motionVector * speed;
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);
                break;
            case playerStates.walking:
                rigidbody2d.velocity += motionVector * speed;
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);
                break;
            case playerStates.spriting:
                rigidbody2d.velocity += motionVector * SprintSpeed;
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, sprintMaxSpeed);
                break;
            case playerStates.dashing:
                //rigidbody2d.velocity += motionVector * DashSpeed;
                rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, dashMaxSpeed);
                break;
            default:
                break;
        }

        switch (playerState)
        {
            case playerStates.idle:
                if (Stamina + RegenStam <= 100f)
                    Stamina += RegenStam;
                break;
            case playerStates.walking:
                if (Stamina + RegenStam <= 100f)
                    Stamina += RegenStam;
                break;
            case playerStates.spriting:
                Stamina -= SprintStam;
                break;
            case playerStates.dashing:
                break;
            default:
                break;
        }

        if (motionVector == new Vector2(0.00f,0.00f))
            rigidbody2d.velocity = Vector3.MoveTowards(rigidbody2d.velocity, Vector3.zero, dragAmount * Time.deltaTime);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Dash()
    {
        if (Stamina < 25f || playerState == playerStates.dashing)
            return;

        Stamina -= 25f;

        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 playerToMouseVector = (mouseScreenPosition - playerScreenPosition).normalized;

        rigidbody2d.velocity = playerToMouseVector * DashSpeed;

        playerState = playerStates.dashing;
        DashTimer = DashTime;
    }

    void Sprint(bool value)
    {
        switch (value)
        {
            case true:
                switch (playerState)
                {
                    case playerStates.idle:
                        playerState = playerStates.spriting;
                        break;
                    case playerStates.walking:
                        playerState = playerStates.spriting;
                        break;
                    case playerStates.spriting:
                        playerState = playerStates.spriting;
                        break;
                    case playerStates.dashing:
                        break;
                    default:
                        break;
                }
                break;
            case false:
                switch (playerState)
                {
                    case playerStates.idle:
                        playerState = playerStates.idle;
                        break;
                    case playerStates.walking:
                        playerState = playerStates.walking;
                        break;
                    case playerStates.spriting:
                        playerState = playerStates.walking;
                        break;
                    case playerStates.dashing:
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}