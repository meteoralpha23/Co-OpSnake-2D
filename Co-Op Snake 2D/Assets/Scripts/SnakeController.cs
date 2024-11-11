using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // To reload the scene

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection;
    private List<Transform> segments;
    public Transform segmentPrefab;
    public GameObject gameOverScreen;  // Reference to the GameOver screen UI
    public ScoreManager scoreManager;  // Reference to the ScoreManager

    private float moveSpeed = 1f; // The default snake speed
    private bool isSpeedBoostActive = false; // Flag for Speed Boost power-up

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        nextDirection = direction;

        // Initially, make sure the GameOver screen is hidden
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Initialize the score
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
    }

    private void Update()
    {
        // Input handling for direction changes
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            nextDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            nextDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            nextDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            nextDirection = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        Move();
        CheckSelfCollision();
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);

        // Increase score when the snake eats food
        if (scoreManager != null)
        {
            scoreManager.IncreaseScore(10); // Add 10 points when snake eats a MassGainer
        }
    }

    private void Move()
    {
        direction = nextDirection;

        // Move the snake with the current speed
        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * moveSpeed;

        if (newPosition.x >= 40) newPosition.x = -40;
        else if (newPosition.x < -40) newPosition.x = 40;
        if (newPosition.y >= 20) newPosition.y = -20;
        else if (newPosition.y < -20) newPosition.y = 20;

        // Move each segment of the snake body
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MassGainer"))
        {
            Grow();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("MassBurner"))
        {
            if (segments.Count > 1)
            {
                Shrink();
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("SpeedUp"))
        {
            // Handle SpeedUp Power-up collection
            ActivateSpeedUp();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ScoreBoost"))
        {
            // Handle ScoreBoost Power-up collection
            ActivateScoreBoost();
            Destroy(other.gameObject);
        }
    }

    private void Shrink()
    {
        if (segments.Count > 1)
        {
            Transform lastSegment = segments[segments.Count - 1];
            segments.RemoveAt(segments.Count - 1);
            Destroy(lastSegment.gameObject);

            // Decrease score when MassBurner is eaten
            if (scoreManager != null)
            {
                scoreManager.DecreaseScore(5); // Subtract 5 points for MassBurner
            }
        }
    }

    private void CheckSelfCollision()
    {
        // Check for self-collision
        for (int i = 1; i < segments.Count; i++)
        {
            if (transform.position == segments[i].position)
            {
                Debug.Log("Self collision detected! Game Over.");
                StartCoroutine(GameOver());  // Call the coroutine for Game Over
                break;
            }
        }
    }

    private IEnumerator GameOver()
    {
        // Display the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Wait for 3 seconds (or adjust this duration if needed)
        yield return new WaitForSeconds(3f);

        // Now the player can manually restart via the restart button (handled by your SceneLoader)
    }

    // Method to activate Speed Up power-up
    private void ActivateSpeedUp()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            moveSpeed *= 1.5f; // Increase the snake speed by 50%
            StartCoroutine(DeactivateSpeedUp());
        }
    }

    // Method to deactivate Speed Up after a duration
    private IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(5f); // Duration for Speed Up (5 seconds)
        moveSpeed /= 1.5f; // Reset the snake speed
        isSpeedBoostActive = false;
    }

    // Method to activate Score Boost power-up
    private void ActivateScoreBoost()
    {
        if (scoreManager != null)
        {
            scoreManager.SetScoreMultiplier(2); // Double the score multiplier
            StartCoroutine(DeactivateScoreBoost());
        }
    }

    // Method to deactivate Score Boost after a duration
    private IEnumerator DeactivateScoreBoost()
    {
        yield return new WaitForSeconds(5f); // Duration for Score Boost (5 seconds)
        if (scoreManager != null)
        {
            scoreManager.ResetMultiplier(); // Reset the multiplier to 1
        }
    }

    public int GetSegmentCount()
    {
        return segments.Count;
    }
}
