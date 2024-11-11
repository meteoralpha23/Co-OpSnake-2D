using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject scoreBoostPrefab;
    public GameObject speedUpPrefab;
    public float powerUpSpawnIntervalMin = 5f;
    public float powerUpSpawnIntervalMax = 10f;
    public ScoreManager scoreManager;

    private bool isScoreBoostActive = false;
    private bool isSpeedUpActive = false;

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            float spawnInterval = Random.Range(powerUpSpawnIntervalMin, powerUpSpawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            GameObject powerUpToSpawn = Random.value > 0.5f ? scoreBoostPrefab : speedUpPrefab;
            Instantiate(powerUpToSpawn, GetRandomPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        return new Vector3(x, y, 0);
    }

    public void ActivateScoreBoost()
    {
        if (!isScoreBoostActive)
        {
            isScoreBoostActive = true;
            scoreManager.SetScoreMultiplier(2);
            StartCoroutine(DeactivateScoreBoost());
        }
    }

    private IEnumerator DeactivateScoreBoost()
    {
        yield return new WaitForSeconds(5f);
        scoreManager.ResetMultiplier();
        isScoreBoostActive = false;
    }

    public void ActivateSpeedUp()
    {
        if (!isSpeedUpActive)
        {
            isSpeedUpActive = true;
            StartCoroutine(DeactivateSpeedUp());
        }
    }

    private IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(5f);
        isSpeedUpActive = false;
    }
}
