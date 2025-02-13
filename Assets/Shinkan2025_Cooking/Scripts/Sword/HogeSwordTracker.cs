using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

public class HogeSwordTracker : MonoBehaviour, ISwordTracker
{
    [SerializeField] FoodObjectListSO _foodObjectListSO;
    [SerializeField] FoodRecipeListSO _foodRecipeListSO;

    private List<string> _foodChildrenName = new();
    private const int MaxFoodCounter = 3;
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;
    public void SubScribeFoodName(ISwordPhysicsHandler swordPhysicsHandler)
    {
         swordPhysicsHandler.IsStabbed
            .Subscribe(x =>
            {
                var name = swordPhysicsHandler.FoodName;
                _foodChildrenName.Add(name);
                Debug.Log($"name={name}");

                if (_foodChildrenName.Count == MaxFoodCounter)
                {
                    Debug.Log("Full");
                    List<FoodDataBaseSO> recipe = new();

                    foreach (var foodname in _foodChildrenName)
                    {
                        recipe.Add(_foodObjectListSO.GetFoodData(foodname));
                    }

                    var hitrecipe = _foodRecipeListSO.GetRecipefromFoodData(recipe);
                    Debug.Log(hitrecipe.FoodRecipeName);
                    _swordFullStabbEvent.OnNext(Unit.Default);

                    swordPhysicsHandler.OnCompletedFood();            
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
