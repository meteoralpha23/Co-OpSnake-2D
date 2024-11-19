using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int scoreMultiplier = 1;

   

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {

    }

    public void IncreaseScore(int amount)
    {
        score += amount * scoreMultiplier;
        UpdateScoreText();
    }

   

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

   

   
    
}
