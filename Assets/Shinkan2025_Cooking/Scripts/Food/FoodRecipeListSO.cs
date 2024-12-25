using System.Collections.Generic;
using UnityEngine;
using System; 
[CreateAssetMenu(menuName = "RecipeList_Scriptable")]
public class FoodRecipeListSO : ScriptableObject
{
    public List<FoodRecipeDataBaseSO> RecipeList = new List<FoodRecipeDataBaseSO>();
    
    public FoodRecipeDataBaseSO GetRecipefromFoodData(List<FoodDataBaseSO> foodList)
    {
		List<FoodRecipeDataBaseSO> temporaryhitrecipe= new();
		int hitcount;
	    int maximumRecipePoint = 0;
		FoodRecipeDataBaseSO resultRecipe = new();
		bool isHit;
	    foreach(FoodRecipeDataBaseSO recipe in RecipeList)
		{
			
			hitcount = 0;
			//そもそも数が一致しないならパス
			if(recipe.FoodRecipe.Count != foodList.Count)continue;
			//基本は名前でサーチするがレシピに名前がないときFoodTypeで判定する
			for(int i = 0;i<foodList.Count;i++)
			{
				isHit = false;//n番目の要素はまだ見つかってない
				if(recipe.FoodRecipe[i].FoodName == "0")
				{
					//FoodTypeを複数持つ可能性あるんでforeach
					foreach(FoodType recipefoodType in recipe.FoodRecipe[i].FoodAtribute)
					{
						foreach(FoodType foodElementType in foodList[i].FoodAtribute)
						{
							if(recipefoodType==foodElementType)
							{
								//既にこの回で見つかってるならカウンターは増加しない
								if(isHit)continue;
								//該当時にカウンター増加
								hitcount++;
								isHit = true;
							}
						}
				    }
				}
				else
				{
					//名前で判定
		    	    if(recipe.FoodRecipe[i].FoodName==foodList[i].FoodName)
					{
					    hitcount++;
					}
				}
		    }
			//レシピ数とカウントが同じなら完全にレシピに合致してる
			if(hitcount>=recipe.FoodRecipe.Count)temporaryhitrecipe.Add(recipe);
				
        }
        //ヒットしたレシピから最も得点が高いブツを抜き出す
	    foreach(FoodRecipeDataBaseSO hitrecipe in temporaryhitrecipe)
	    {
			if(hitrecipe.RecipePoint >= maximumRecipePoint)
		    {
				maximumRecipePoint = hitrecipe.RecipePoint;
			    resultRecipe = hitrecipe;
		    }
	    }
		return resultRecipe;
    }
}
