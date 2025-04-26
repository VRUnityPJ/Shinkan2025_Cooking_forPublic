using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Shinkan2025_Cooking.Scripts.Points;
using Shinkan2025_Cooking.Scripts.Sword;
public class HogeSwordTracker : MonoBehaviour, ISwordTracker
{

    private List<string> _foodChildrenName = new();
    private int count = 0;
    private const int MaxFoodCounter = 3;
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;
    public void SubScribeFoodName(ISwordPhysicsHandler swordPhysicsHandler)
    {
         swordPhysicsHandler.IsStabbed
            // .Skip(1)
            .Subscribe(x =>
            {
                count++;
                var name = swordPhysicsHandler.FoodName;
                _foodChildrenName.Add(name);
                Debug.Log($"name={name}"+count);

                if (count >= MaxFoodCounter)
                {
                    RecipeChecker.Instance?.RecipeCheck(_foodChildrenName);

                    swordPhysicsHandler.OnCompletedFood();   
                    _swordFullStabbEvent.OnNext(Unit.Default);
                    _foodChildrenName.Clear();   
                    count = 0;
                    
                }
            })
            .AddTo(this);
    }

    public void OnStabbed(string name, GameObject foodObj)
    {
        
    }

    public void TestDestroy(Transform root)
    {
        
    }
}
