using Ranking.Scripts;
using Ranking.Scripts.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shinkan2025_Cooking.Scripts.Points
{
    /// <summary>
    /// Recipeからポイントを決定しpointholderに投げるクラス
    /// 特定のSceneに一つだけ存在する
    /// </summary>
    public class RecipeChecker : MonoBehaviour
    {
        
        [SerializeField]FoodObjectListSO foodObjectListSO;
        [SerializeField]FoodRecipeListSO foodRecipeListSO;
        public static RecipeChecker Instance;
        private int _recipePoint;
        
        void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            SceneManager.sceneUnloaded += DestroyInstance;
        }
        public void RecipeCheck(List<string> foods)
        {
            _recipePoint = 0;
            List<FoodDataBaseSO> recipe = new();
            foreach (var foodname in foods)
            {
                FoodDataBaseSO foodData = foodObjectListSO.GetFoodData(foodname);
                recipe.Add(foodData);
                _recipePoint += foodData.FoodBasePoint;
            }
            var hitrecipe = foodRecipeListSO.GetRecipefromFoodData(recipe);
            Debug.Log(hitrecipe.FoodRecipeName);
            _recipePoint += hitrecipe.RecipePoint;
            PointHolder.Instance?.UpPoint(_recipePoint);
        }

        private void DestroyInstance(Scene _)
        {
            Destroy(Instance);
        }
    }
}
