using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [Header("Speed")]
    [SerializeField] float speed = 2f;
    [SerializeField] float maxSpeed = 10f;

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
    }

    void FixedUpdate()
    {
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

        print(rigidbody2d.velocity.magnitude);
    }
}