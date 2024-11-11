using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject scoreBoostPrefab;
    public GameObject speedUpPrefab;
    public float powerUpSpawnIntervalMin = 5f; // Minimum interval time to spawn power-ups
    public float powerUpSpawnIntervalMax = 10f; // Maximum interval time to spawn power-ups
    public ScoreManager scoreManager; // Reference to ScoreManager to manage the score multiplier

    private bool isScoreBoostActive = false;
    private bool isSpeedUpActive = false;

    private void Start()
    {
        // Start spawning power-ups
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            // Randomly decide when to spawn a power-up
            float spawnInterval = Random.Range(powerUpSpawnIntervalMin, powerUpSpawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            // Randomly choose a power-up to spawn
            GameObject powerUpToSpawn = Random.value > 0.5f ? scoreBoostPrefab : speedUpPrefab;
            Instantiate(powerUpToSpawn, GetRandomPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Get a random position within the bounds of your game world (adjust as needed)
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        return new Vector3(x, y, 0);
    }

    // Method to activate Score Boost power-up
    public void ActivateScoreBoost()
    {
        if (!isScoreBoostActive)
        {
            isScoreBoostActive = true;
            scoreManager.SetScoreMultiplier(2);  // Double the score multiplier
            StartCoroutine(DeactivateScoreBoost());
        }
    }

    // Method to deactivate Score Boost power-up after a duration
    private IEnumerator DeactivateScoreBoost()
    {
        yield return new WaitForSeconds(5f);  // Duration for the Score Boost (e.g., 5 seconds)
        scoreManager.ResetMultiplier();  // Reset multiplier to 1
        isScoreBoostActive = false;
    }

    // Method to activate Speed Up power-up
    public void ActivateSpeedUp()
    {
        if (!isSpeedUpActive)
        {
            isSpeedUpActive = true;
            // Increase snake speed logic here (you can call your SnakeController's SpeedUp method)
            // For example: snakeController.IncreaseSpeed(2); 
            StartCoroutine(DeactivateSpeedUp());
        }
    }

    // Method to deactivate Speed Up power-up after a duration
    private IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(5f);  // Duration for the Speed Up (e.g., 5 seconds)
        isSpeedUpActive = false;
        // Reset speed logic here (e.g., snakeController.ResetSpeed();)
    }
}
