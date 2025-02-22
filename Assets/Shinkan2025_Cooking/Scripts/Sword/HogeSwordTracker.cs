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
    private List<List<string>> _recipeNameList = new();
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
                    Debug.Log("Full");
                    List<FoodDataBaseSO> recipe = new();

                    foreach (var foodname in _foodChildrenName)
                    {
                        recipe.Add(_foodObjectListSO.GetFoodData(foodname));
                    }

                    var hitrecipe = _foodRecipeListSO.GetRecipefromFoodData(recipe);
                    Debug.Log(hitrecipe.FoodRecipeName);
                    

                    swordPhysicsHandler.OnCompletedFood();    
                    _swordFullStabbEvent.OnNext(Unit.Default);
                    //ここで名前のリストを保存 
                    _recipeNameList.Add(_foodChildrenName);
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
