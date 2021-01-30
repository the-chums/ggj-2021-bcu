using UnityEngine;

using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterMovement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float MaxVelocity = 5f;
    public Transform CharacterFace;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float accelerationDelta = Acceleration * Time.deltaTime;

        float accelerationX = horizontalInput * accelerationDelta;
        float accelerationY = verticalInput * accelerationDelta;
        Vector2 acceleration = new Vector2(accelerationX, accelerationY);

        _rigidbody.velocity += acceleration;

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
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, MaxVelocity);
    }
}
