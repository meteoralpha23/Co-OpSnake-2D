using Unity.Mathematics;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;

    [SerializeField] private float fixedSpeed = 10f; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * fixedSpeed;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
       
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

      
        rb.velocity = direction * fixedSpeed;
    }
}
