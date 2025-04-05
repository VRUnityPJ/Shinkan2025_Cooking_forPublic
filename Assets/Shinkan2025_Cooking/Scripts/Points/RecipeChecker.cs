using Ranking.Scripts;
using Ranking.Scripts.Interface;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Shinkan2025_Cooking.Scripts.Points
{
    /// <summary>
    /// Recipeからポイントを決定しpointholderに投げるクラス
    /// 特定のSceneに一つだけ存在する
    /// </summary>
    public class RecipeChecker : MonoBehaviour, IRecipeObservable
    {
        
        [SerializeField]FoodObjectListSO foodObjectListSO;
        [SerializeField]FoodRecipeListSO foodRecipeListSO;

        public static RecipeChecker Instance;
        public IObservable<string> RecipeName => _recipeName;

        private readonly Subject<string> _recipeName = new();

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
            
            RegisterRecipeName(hitrecipe.FoodRecipeName);
            _recipePoint += hitrecipe.RecipePoint;
            PointHolder.Instance?.UpPoint(_recipePoint);
        }

        private void DestroyInstance(Scene _)
        {
            Destroy(Instance);
        }

        public void RegisterRecipeName(string recipeName)
        {

            _recipeName.OnNext(recipeName);
            Debug.Log(recipeName);
        }
    }
}
