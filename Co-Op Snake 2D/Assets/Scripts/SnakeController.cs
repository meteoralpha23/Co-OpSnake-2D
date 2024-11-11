using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection;
    private List<Transform> segments;
    public Transform segmentPrefab;
    public GameObject gameOverScreen;
    public ScoreManager scoreManager;

    private float moveSpeed = 1f;
    private bool isSpeedBoostActive = false;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);
        nextDirection = direction;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
    }

    private void Update()
    {
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

        if (scoreManager != null)
        {
            scoreManager.IncreaseScore(10);
        }
    }

    private void Move()
    {
        direction = nextDirection;

        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * moveSpeed;

        if (newPosition.x >= 40) newPosition.x = -40;
        else if (newPosition.x < -40) newPosition.x = 40;
        if (newPosition.y >= 20) newPosition.y = -20;
        else if (newPosition.y < -20) newPosition.y = 20;

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
            ActivateSpeedUp();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ScoreBoost"))
        {
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

            if (scoreManager != null)
            {
                scoreManager.DecreaseScore(5);
            }
        }
    }

    private void CheckSelfCollision()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            if (transform.position == segments[i].position)
            {
                Debug.Log("Self collision detected! Game Over.");
                StartCoroutine(GameOver());
                break;
            }
        }
    }

    private IEnumerator GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        yield return new WaitForSeconds(3f);
    }

    private void ActivateSpeedUp()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            moveSpeed *= 1.5f;
            StartCoroutine(DeactivateSpeedUp());
        }
    }

    private IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(5f);
        moveSpeed /= 1.5f;
        isSpeedBoostActive = false;
    }

    private void ActivateScoreBoost()
    {
        if (scoreManager != null)
        {
            scoreManager.SetScoreMultiplier(2);
            StartCoroutine(DeactivateScoreBoost());
        }
    }

    private IEnumerator DeactivateScoreBoost()
    {
        yield return new WaitForSeconds(5f);
        if (scoreManager != null)
        {
            scoreManager.ResetMultiplier();
        }
    }

    public int GetSegmentCount()
    {
        return segments.Count;
    }
}
