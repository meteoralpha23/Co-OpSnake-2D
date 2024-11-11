using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;// To access UI components
using UnityEngine.SceneManagement; // Make sure to include this for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // This will hold a reference to the TMP component
    private int score = 0;
    private int scoreMultiplier = 1;  // Default multiplier is 1

    public GameObject winScreen;

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        if (score > 500f && !winScreen.activeSelf)
        {
            // Show the win screen and start the coroutine to restart the scene
            winScreen.SetActive(true);
            StartCoroutine(WaitAndRestart(5f)); // Wait for 5 seconds before restarting
        }
    }

    // Method to increase score, taking into account the multiplier
    public void IncreaseScore(int amount)
    {
        score += amount * scoreMultiplier;  // Apply the score multiplier
        UpdateScoreText();
    }

    // Method to decrease score (if needed, like for a MassBurner)
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Method to reset the score (for game over or reset)
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }

    // Method to set the score multiplier (for Score Boost power-up)
    public void SetScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;  // Set multiplier to 2 when score boost is active, else 1
    }

    // Optional: Reset the score multiplier back to default (1) after boost ends
    public void ResetMultiplier()
    {
        scoreMultiplier = 1;
    }

    // Coroutine to wait for a specified time before restarting the scene
    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);  // Wait for the specified time (5 seconds)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Restart the scene
    }
}
