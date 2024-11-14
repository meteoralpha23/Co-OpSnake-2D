using Unity.Mathematics;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;

    [SerializeField] private float fixedSpeed = 10f; // Set a fixed speed for the ball

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Apply an initial velocity in a random direction with the specified fixed speed
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * fixedSpeed;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // Calculate the reflection direction
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

        // Set the velocity to the fixed speed in the reflection direction
        rb.velocity = direction * fixedSpeed;
    }
}
