using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public TextMeshProUGUI gameOverText;

    [SerializeField] private Collider2D movementArea; 
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] public GameObject loseScreen;
    [SerializeField] private AudioClip audioClip;

    private float vertical, horizontal;
    private Rigidbody2D rb;
    private Bounds movementBounds;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();

        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        if (movementArea != null)
        {
            movementBounds = movementArea.bounds; 
        }
        else
        {
            Debug.LogError("Movement area (Collider2D) is not assigned!");
        }
    }

    void Update()
    {
        MoveBat();
    }

    private void MoveBat()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal * moveSpeed, vertical * moveSpeed) * Time.deltaTime;
        Vector2 newPosition = rb.position + movement;

      
        if (movementArea != null)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, movementBounds.min.x, movementBounds.max.x);
            newPosition.y = Mathf.Clamp(newPosition.y, movementBounds.min.y, movementBounds.max.y);
        }

       
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("Game Over");
            loseScreen.SetActive(true);
            GameOverScore();
        }
        else if (collision.CompareTag("Apple"))
        {
            scoreManager.IncreaseScore(5);
            SoundManager.Instance.PlaySoundEffect(audioClip);
            Destroy(collision.gameObject);
        }
    }

    private void GameOverScore()
    {
        gameOverText.gameObject.SetActive(true);
        int gameOverScore = scoreManager.GetScore();
        gameOverText.text = "Your Score is :: " + gameOverScore.ToString();
    }
}
