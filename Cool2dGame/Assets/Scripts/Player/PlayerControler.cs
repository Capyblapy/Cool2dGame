using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [Header("General Stuff")]
    [SerializeField] int Health = 100;
    [SerializeField] float Stamina = 100f;

    [Header("WASD Movment")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 10f;

    [Header("Dash")]
    [SerializeField] string DashKey = "space";
    [SerializeField] float DashTime = 5f;
    [SerializeField] float DashSpeed = 15f;
    [SerializeField] bool canDash = true;

    [Header("Running")]
    [SerializeField] string SprintKey = "left shift";
    [SerializeField] int SprintStam = 10;
    [SerializeField] float SprintSpeed = 10f;
    [SerializeField] bool Sprinting = false;

    Vector2 motionVector;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector3(
            horizontal,
            vertical,
            0
        );

        if (Input.GetKeyDown(DashKey))
            Dash();

        if (Input.GetKeyDown(SprintKey))
            Sprinting = !Sprinting;
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

    private void Move()
    {
        if (Sprinting == false)
            rigidbody2d.velocity = motionVector * speed;
        else if (Sprinting == true)
        {
            if (Stamina - 0.05f <= 0)
            {
                Sprinting = false;
                rigidbody2d.velocity = motionVector * speed;
            }
            else
            {
                rigidbody2d.velocity = motionVector * SprintSpeed;
                Stamina -= 0.05f;
            }
        }

        print(rigidbody2d.velocity);
        rigidbody2d.velocity =  Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);

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

        // Dash Code Here

        canDash = true;
    }
}