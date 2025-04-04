using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class ResultRecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipenameText;
    [SerializeField] private TextMeshProUGUI _recipePointsText;
    public void RecipeText(string name,int point)
    {
        _recipenameText.text = name;
        _recipePointsText.text = $"{point.ToString()}Point";
    }
}
