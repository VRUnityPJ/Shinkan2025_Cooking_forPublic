using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using Unity.VisualScripting;
using UnityEngine.Playables;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private IGameProgressIndicatable _gameProgressIndicatable;
    [SerializeField] private GameObject gameProgressIndicatorPrefab;
    [SerializeField] private FoodObjectListSO foodObjList;
    [SerializeField] private float foodSpeed = 5.0f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float foodLifeTime = 10.0f;
    [SerializeField] private List<PlayableDirector> _directors;
    [SerializeField] private float _time = 0.9f;
    
    //private GameObject food;
    private float time = 0.0f;
    private float nextFoodSpawnTime = 1.0f;
    public bool isFoodSpawnable  = false;
    private CancellationToken _token = new();

    void Awake()
    {
        _gameProgressIndicatable = gameProgressIndicatorPrefab.GetComponent<IGameProgressIndicatable>();
        isFoodSpawnable = false;
        
        _gameProgressIndicatable.OnStartGame
            .Subscribe(_ =>
            {
                isFoodSpawnable = true;
            });
    }
    
    void Update()
    {
        if (isFoodSpawnable)
        {
            Debug.Log("Food spawn");
            SpawnFood();
        }
            
        
    }

    private void SpawnFood()
    {
        time += Time.deltaTime;
        if (time >= nextFoodSpawnTime)
        {
            _token = this.GetCancellationTokenOnDestroy();
            NextFood();
            int randomPointFirst = UnityEngine.Random.Range(0, spawnPoints.Length);
            int randomPointSecond;

            // 無限ループを避けるため、範囲が2以上であることを確認
            if (spawnPoints.Length > 1)
            {
                do
                {
                    randomPointSecond = UnityEngine.Random.Range(0, spawnPoints.Length);
                } while (randomPointFirst == randomPointSecond);
            }
            else
            {
                // 範囲が1以下の場合、安全にデフォルト値を設定
                randomPointSecond = randomPointFirst;
            }
            //0秒
            //アニメーション開始
            _directors[randomPointFirst].Play();
            _directors[randomPointSecond].Play();
            //炒める時間まつ

           
            InstantiateFoodAsync(_token,randomPointFirst);
            InstantiateFoodAsync(_token,randomPointSecond);
            time = 0.0f;
        }
    }
    public async void InstantiateFoodAsync(CancellationToken token,int spawnPointNum)
    {
            await UniTask.Delay(TimeSpan.FromSeconds(_time), cancellationToken: token);
            Transform randomSpawnPoint = spawnPoints[spawnPointNum];

            int index = UnityEngine.Random.Range(0, foodObjList.foodList.Count);
            var food = foodObjList.foodList[index];

            GameObject spawnedFood = Instantiate(food, randomSpawnPoint);
            spawnedFood.TryGetComponent<IFoodPhysicsHandler>(out var handler);
            handler.OnInstantiate(foodSpeed, foodLifeTime);
            Debug.Log("timeゲーム終了");
    }
  

    private void NextFood()
    {
        nextFoodSpawnTime = UnityEngine.Random.Range(2.5f, 5.0f);

    }
}