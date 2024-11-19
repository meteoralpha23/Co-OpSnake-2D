using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodSpawner : MonoBehaviour
{
    public GameObject Spawner;

    public BoxCollider2D gridArea;
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

            if (Spawner != null)
            {

                SpawnFoodItem(Spawner);

            }
            else
            {
                Debug.Log("Spawner is null");
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
