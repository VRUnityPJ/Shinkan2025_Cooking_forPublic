using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Shinkan2025_Cooking.Scripts.Points;

public class TestSwordTracker : MonoBehaviour,ISwordTracker
{
    // Start is called before the first frame update
    List<string> FoodChildrenName=new();
    [SerializeField]List<GameObject> FoodParentPoint = new();
    [SerializeField]FoodObjectListSO foodObjectListSO;
    [SerializeField]FoodRecipeListSO foodRecipeListSO;
    int FoodCounter = 0;
    const int MaxFoodCounter = 3;
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;
    public void OnStabbed(string name,GameObject foodObj)
    {
        FoodChildrenName.Add(name);
        Debug.Log(name);
        foodObj.transform.parent = FoodParentPoint[FoodCounter].transform;
        foodObj.transform.position = FoodParentPoint[FoodCounter].transform.position;
        FoodCounter++;
        if(FoodCounter==MaxFoodCounter)
        {
            Debug.Log("Full");
            RecipeChecker.Instance?.RecipeCheck(FoodChildrenName);
            FoodChildrenName.Clear();
            _swordFullStabbEvent.OnNext(Unit.Default);
            TestDestroy(this.transform);
        }
        
    }
    

    　// rootの子オブジェクトをすべてDestroyする
    public void TestDestroy(Transform root)
    {
        //自分の子供を全て調べる
        foreach (Transform child in root)
        {
            //自分の子供をDestroyする
            Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void SubScribeFoodName(ISwordPhysicsHandler swordPhysicsHandler)
    {
        throw new NotImplementedException();
    }
}
