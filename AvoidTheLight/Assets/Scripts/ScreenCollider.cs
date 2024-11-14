using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
   [SerializeField] private BoxCollider2D leftCollider, rightCollider, topCollider, bottomCollider;
    private Vector2 lastScreenSize;

    void Awake()
    {
        // Add four BoxCollider2D components for screen edges
        leftCollider = gameObject.AddComponent<BoxCollider2D>();
        rightCollider = gameObject.AddComponent<BoxCollider2D>();
        topCollider = gameObject.AddComponent<BoxCollider2D>();
        bottomCollider = gameObject.AddComponent<BoxCollider2D>();

        // Set initial positions and sizes for the colliders
        UpdateColliders();
    }

    void Update()
    {
        // Check if screen resolution has changed
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
        {
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            UpdateColliders();
        }
    }

    // Position and resize the colliders based on the current screen size
    void UpdateColliders()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        // Convert screen edges to world points
        Vector2 bottomLeft = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 topRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float screenWidth = topRight.x - bottomLeft.x;
        float screenHeight = topRight.y - bottomLeft.y;

        // Set up each collider to cover one screen edge

        // Left collider
        leftCollider.size = new Vector2(0.1f, screenHeight);
        leftCollider.offset = new Vector2(bottomLeft.x - 0.05f, 0);

        // Right collider
        rightCollider.size = new Vector2(0.1f, screenHeight);
        rightCollider.offset = new Vector2(topRight.x + 0.05f, 0);

        // Top collider
        topCollider.size = new Vector2(screenWidth, 0.1f);
        topCollider.offset = new Vector2(0, topRight.y + 0.05f);

        // Bottom collider
        bottomCollider.size = new Vector2(screenWidth, 0.1f);
        bottomCollider.offset = new Vector2(0, bottomLeft.y - 0.05f);
    }

    // Runs when colliding if collider is not set to Trigger
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
        if (collidingRB != null)
        {
            // Reflect the velocity based on the collision contact point's normal
            Vector2 reflectedVelocity = Vector2.Reflect(collidingRB.velocity, collision.contacts[0].normal);
            collidingRB.velocity = reflectedVelocity;
            Debug.Log("Collision detected and velocity reflected.");
        }
    }

    // Runs when colliding if collider is set to Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        if (colliderRB != null)
        {
            // Reflect the velocity based on the incoming velocity
            colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, -colliderRB.velocity.normalized);
            Debug.Log("Trigger collision detected and velocity reflected.");
        }
    }
}
