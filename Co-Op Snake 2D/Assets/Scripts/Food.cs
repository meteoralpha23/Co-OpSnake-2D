using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject massGainerPrefab;
    public GameObject massBurnerPrefab;
    public BoxCollider2D gridArea;
    public SnakeController snakeController;

    public float spawnInterval = 3f;
    public float foodLifetime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (snakeController.GetSegmentCount() > 1)
            {
                if (Random.value > 0.5f)
                {
                    SpawnFoodItem(massGainerPrefab);
                }
                else
                {
                    SpawnFoodItem(massBurnerPrefab);
                }
            }
            else
            {
                SpawnFoodItem(massGainerPrefab);
            }
        }
    }

    private void SpawnFoodItem(GameObject foodPrefab)
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 spawnPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        GameObject foodItem = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

        StartCoroutine(DestroyFoodAfterTime(foodItem, foodLifetime));
    }

    private IEnumerator DestroyFoodAfterTime(GameObject foodItem, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(foodItem);
    }
}
