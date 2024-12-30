using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public List<GameObject> foodList;
    private GameObject food;

    public float foodLifeTime = 10.0f;
    private float time = 0.0f;

    private float nextFoodSpawnTime = 5.0f;
    private float foodSpeed;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= nextFoodSpawnTime)
        {
            NextFood();
            InstantiateFood();
            time = 0.0f;
        }
    }

    private void InstantiateFood()
    {
        GameObject spawnedFood = Instantiate(food, transform.position, Quaternion.identity);
        FoodPhysicsHandler handler = spawnedFood.GetComponent<FoodPhysicsHandler>();

        handler.OnInstantiate(foodSpeed, foodLifeTime);
    }

    private void NextFood()
    {
        nextFoodSpawnTime = Random.Range(1.0f, 5.0f);

        foodSpeed = Random.Range(1.0f, 5.0f);

        int index = Random.Range(0, foodList.Count);
        food = foodList[index];
    }
}