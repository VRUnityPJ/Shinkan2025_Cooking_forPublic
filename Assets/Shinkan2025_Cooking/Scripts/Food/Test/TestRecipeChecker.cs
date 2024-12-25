using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReciopeChecker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private List<FoodDataBaseSO> _testRecipe;
    private FoodRecipeDataBaseSO _recipeData;
    private FoodRecipeListSO _foodRecipeListSO;
    void Start()
    {
        _foodRecipeListSO = Resources.Load<FoodRecipeListSO>("FoodRecipeListSO");
        _recipeData = _foodRecipeListSO.GetRecipefromFoodData(_testRecipe);
            Debug.Log("ヒットしたレシピ名:"+ _recipeData.FoodRecipeName+"ポイント"+_recipeData.RecipePoint);
        
    }
}
