using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
[System.Serializable]
[CreateAssetMenu(fileName = "FoodDataBase")]
public class FoodDataBaseSO : ScriptableObject
{

    /// <summary>
    /// Foodの名前
    /// </summary>
    [SerializeField]private string _foodName;
    
    public string FoodName{get => _foodName;}
    /// <summary>
    /// Foodの名前
    /// </summary>
    [SerializeField]private List<FoodType> _foodAtribute;
    
    public List<FoodType> FoodAtribute{get => _foodAtribute;}
    /// <summary>
    /// Foodの名前
    /// </summary>
    [SerializeField]private GameObject _foodObject;
    
    public GameObject FoodObject{get => _foodObject;}
    /// <summary>
    /// Foodの名前
    /// </summary>
    [SerializeField]private int _foodBasePoint;
    
    public int FoodBasePoint{get => _foodBasePoint;}
    
}
