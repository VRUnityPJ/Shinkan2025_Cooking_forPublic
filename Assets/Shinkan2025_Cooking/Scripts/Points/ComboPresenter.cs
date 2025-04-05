using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinkan2025_Cooking.Scripts.Points;
using VContainer;
using UniRx;

public class ComboPresenter : MonoBehaviour
{
    [SerializeField] private ComboFoodText _comboFoodText;
    [Inject] private IRecipeObservable _recipeObservable;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_recipeObservable == null) return; 

        _recipeObservable.RecipeName
            .Where(name => name != null)
            .Subscribe(name =>
            {
                _comboFoodText.ShowComboText(name);

            }).AddTo(this);
    }
}
