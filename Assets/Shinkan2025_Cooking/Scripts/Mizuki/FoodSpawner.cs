using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private FoodObjectListSO foodObjList;
    private GameObject food;
    public float foodLifeTime = 10.0f;
    private float time = 0.0f;
    private float nextFoodSpawnTime = 5.0f;
    [SerializeField] private float foodSpeed = 10.0f;

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
            int randomPointFirst = Random.Range(0, spawnPoints.Length);
            int randomPointSecond;

            // 無限ループを避けるため、範囲が2以上であることを確認
            if (spawnPoints.Length > 1)
            {
                do
                {
                    randomPointSecond = Random.Range(0, spawnPoints.Length);
                } while (randomPointFirst == randomPointSecond);
            }
            else
            {
                // 範囲が1以下の場合、安全にデフォルト値を設定
                randomPointSecond = randomPointFirst;
            }

            InstantiateFood(randomPointFirst);
            InstantiateFood(randomPointSecond);
            time = 0.0f;
        }
    }

    private void InstantiateFood(int spawnPointNum)
    {
        
        // �����_���ȃX�|�[���|�C���g��I��
        Transform randomSpawnPoint = spawnPoints[spawnPointNum];

        GameObject spawnedFood = Instantiate(food, randomSpawnPoint);
        spawnedFood.TryGetComponent<IFoodPhysicsHandler>(out var handler);
        handler.OnInstantiate(foodSpeed, foodLifeTime);
    }

    private void NextFood()
    {
        nextFoodSpawnTime = Random.Range(2.5f, 5.0f);
        //foodSpeed = Random.Range(5.0f, 10.0f);
        int index = Random.Range(0, foodObjList.foodList.Count);
        food = foodObjList.foodList[index];
    }
}