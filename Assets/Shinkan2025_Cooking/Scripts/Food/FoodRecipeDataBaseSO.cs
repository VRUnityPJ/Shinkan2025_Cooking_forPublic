using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
[System.Serializable]
[CreateAssetMenu(fileName = "FoodRecipeData")]
public class FoodRecipeDataBaseSO : ScriptableObject
{
    /// <summary>
    /// レシピ内容
    /// </summary>
    [SerializeField]private List<FoodDataBaseSO> _foodRecipe;

    public List<FoodDataBaseSO> FoodRecipe{get => _foodRecipe;}
    /// <summary>
    /// レシピ名
    /// </summary>
    [SerializeField]private string _foodRecipeName;

    public string FoodRecipeName{get => _foodRecipeName;}

		/// <summary>
    /// レシピの付加得点
    /// </summary>
    [SerializeField]private int _recipePoint;

    public int RecipePoint{get => _recipePoint;}


}
