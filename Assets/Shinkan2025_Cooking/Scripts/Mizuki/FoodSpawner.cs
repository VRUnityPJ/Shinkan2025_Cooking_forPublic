using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<FoodDataBaseSO> foodList;
    private GameObject food;
    public float foodLifeTime = 10.0f;
    private float time = 0.0f;
    private float nextFoodSpawnTime = 5.0f;
    [SerializeField] private float foodSpeed = 5.0f;

    // スポーンポイントを格納する配列
    private Transform[] spawnPoints;

    void Start()
    {
        // 開始時に子オブジェクトのスポーンポイントを取得
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }
    }

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
        // ランダムなスポーンポイントを選択
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedFood = Instantiate(food, randomSpawnPoint.position, Quaternion.identity);
        spawnedFood.TryGetComponent<IFoodPhysicsHandler>(out var handler);
        handler.OnInstantiate(foodSpeed, foodLifeTime);
    }

    private void NextFood()
    {
        nextFoodSpawnTime = Random.Range(1.0f, 5.0f);
        foodSpeed = Random.Range(5.0f, 10.0f);
        int index = Random.Range(0, foodList.Count);
        food = foodList[index].FoodObject;
    }
}