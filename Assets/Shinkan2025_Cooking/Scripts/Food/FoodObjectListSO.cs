using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FoodObjectList_Scriptable")]
public class FoodObjectListSO : ScriptableObject
{
    public List<GameObject> foodList = new List<GameObject>();

    public FoodDataBaseSO GetFoodData(string name)
    {
        foreach (var foodObj in foodList)
        {
            foodObj.TryGetComponent<Food>(out var food);
            var database = food.GetDataBase();
            if(database.FoodName == name)return database;
        }
        return null;
    }
}
