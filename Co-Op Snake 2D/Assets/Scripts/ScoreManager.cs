using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int scoreMultiplier = 1;

    public GameObject winScreen;

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        if (score > 500f && !winScreen.activeSelf)
        {
            winScreen.SetActive(true);
            StartCoroutine(WaitAndRestart(5f));
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount * scoreMultiplier;
        UpdateScoreText();
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
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

    public void SetScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
    }

    public void ResetMultiplier()
    {
        scoreMultiplier = 1;
    }

    private IEnumerator WaitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
