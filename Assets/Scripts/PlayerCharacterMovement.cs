using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterMovement : MonoBehaviour
{
    public float Acceleration = 25f;
    public float MaxVelocity = 8f;
    public float MaxTimeToSpeed = 0.15f;
    public float decelerationSpeed = 15f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {

            float accelerationDelta = Acceleration / MaxTimeToSpeed * Time.deltaTime;

            float accelerationX = horizontalInput * accelerationDelta;
            float accelerationY = verticalInput * accelerationDelta;
            Vector2 acceleration = new Vector2(accelerationX, accelerationY);

            rb2d.velocity += acceleration;

            float zRotation = transform.eulerAngles.z;

            if (horizontalInput > 0)
            {
                zRotation = -90;
            }
            else if (horizontalInput < 0)
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
        else
        {
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, Vector2.zero, decelerationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, MaxVelocity);
    }
}
