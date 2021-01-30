using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterMovement : MonoBehaviour
{
    public float Acceleration = 15f;
    public float MaxVelocity = 6f;
    public float MaxTimeToSpeed = 0.2f;
    
    private Rigidbody2D RigidBody;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float accelerationDelta = Acceleration / MaxTimeToSpeed * Time.deltaTime;

        float accelerationX = horizontalInput * accelerationDelta;
        float accelerationY = verticalInput * accelerationDelta;
        Vector2 acceleration = new Vector2(accelerationX, accelerationY);

        RigidBody.velocity += acceleration;

        float zRotation = transform.eulerAngles.z;

        if (horizontalInput > 0)
        {
            zRotation = -90;
        }
        else if(horizontalInput < 0)
        {
            zRotation = 90;
        }

        if (verticalInput > 0)
        {
            zRotation = 0;
        }
        else if (verticalInput < 0)
        {
            zRotation = 180;
        }

        transform.rotation = UnityEngine.Quaternion.Euler(0, 0, zRotation);
    }

    void FixedUpdate()
    {
        RigidBody.velocity = Vector2.ClampMagnitude(RigidBody.velocity, MaxVelocity);
    }
}
