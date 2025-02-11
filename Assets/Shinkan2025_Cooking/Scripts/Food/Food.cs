using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]private FoodDataBaseSO foodDataBaseSO;

    public string GetName()
    {
        return foodDataBaseSO.FoodName;
    }
    public FoodDataBaseSO GetDataBase()
    {
        return foodDataBaseSO;
    }
}
