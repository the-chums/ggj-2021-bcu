using UnityEngine;

public class POCCharController : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxVelocity = 5f;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float accelerationX = Input.GetAxis("Horizontal") * acceleration;
        float accelerationY = Input.GetAxis("Vertical") * acceleration;

        accelerationX *= Time.deltaTime;
        accelerationY *= Time.deltaTime;

        // Update rigidbody velocity
        _rigidbody.velocity += new Vector2(accelerationX, accelerationY);
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }
}
