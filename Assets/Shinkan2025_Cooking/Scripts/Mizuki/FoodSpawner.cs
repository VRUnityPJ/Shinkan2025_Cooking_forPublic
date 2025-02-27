using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private FoodObjectListSO foodObjList;
    private GameObject food;
    public float foodLifeTime = 10.0f;
    private float time = 0.0f;
    private float nextFoodSpawnTime = 5.0f;
    [SerializeField] private float foodSpeed = 5.0f;

    private Transform[] spawnPoints;

    void Start()
    {
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
        // �����_���ȃX�|�[���|�C���g��I��
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedFood = Instantiate(food, randomSpawnPoint.position, Quaternion.identity);
        spawnedFood.TryGetComponent<IFoodPhysicsHandler>(out var handler);
        handler.OnInstantiate(foodSpeed, foodLifeTime);
    }

    private void NextFood()
    {
        nextFoodSpawnTime = Random.Range(1.0f, 5.0f);
        foodSpeed = Random.Range(5.0f, 10.0f);
        int index = Random.Range(0, foodObjList.foodList.Count);
        food = foodObjList.foodList[index];
    }
}