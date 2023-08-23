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
    [SerializeField] float speed = 2f;
    [SerializeField] float maxSpeed = 10f;

    [Header("Dash")]
    [SerializeField] string DashKey = "space";
    [SerializeField] float DashDistance = 5f;

    [Header("Running")]
    [SerializeField] string SprintKey = "leftShift";
    [SerializeField] int SprintStam = 10;
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
    }

    void FixedUpdate()
    {
       if (Sprinting == false)
        {
            if (Stamina + 0.5f <= 100f)
                Stamina += 0.5f;
        }

        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity   = motionVector * speed;
        rigidbody2d.velocity =  Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Dash()
    {
        if (Stamina < 25f)
            return;

        Stamina -= 25f;
        print(Stamina);
    }
}