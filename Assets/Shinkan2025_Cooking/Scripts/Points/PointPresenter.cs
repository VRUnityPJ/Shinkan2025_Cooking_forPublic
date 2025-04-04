using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Shinkan2025_Cooking.Scripts.Points;
public class PointPresenter : MonoBehaviour
{
    [SerializeField] PointText _pointText;
    [SerializeField] PointHolder _pointHolder;
    [SerializeField] RecipeChecker _recipeChecker;
    [SerializeField] GameObject _resultPanel;
    [SerializeField] GameObject _recipeResultPrefab;
    [SerializeField] GameObject _instantiatepoint;
    [SerializeField] private TextMeshProUGUI _totalPointText;
    private List<int> _recipePointList=new();
    private List<string> _recipeNameList = new();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _resultPanel.SetActive(false);
        _pointHolder.Point
            .Skip(1)
            .Subscribe(currentPoint =>
            {
                _pointText.CountPointText(currentPoint.IntValue.ToString());
                _totalPointText.text = $"Result {currentPoint.IntValue.ToString()}";
            })
            .AddTo(this);
        _pointHolder.UPPoint
            .Skip(1)
            .Subscribe(upPoint =>
            {
                _recipePointList.Add(upPoint);
             })
             .AddTo(this);

        _recipeChecker.FinishedRecipeName
            .Skip(1)
            .Subscribe(r_name =>
            {
                _recipeNameList.Add(r_name);
             })
            .AddTo(this);
        
    }
    [ContextMenu("DebugTimeUp")]
    public void OnTimeUp()
    {
        _resultPanel.SetActive(true);
        for(int i = 0; i<_recipePointList.Count;i++)
        {
            var recipetext = Instantiate(_recipeResultPrefab,_instantiatepoint.gameObject.transform);
            var recipetextUI = recipetext.GetComponent<ResultRecipeUI>();
            recipetextUI.RecipeText(_recipeNameList[i],_recipePointList[i]);
        }
        
    }
    [ContextMenu("DebugUI")]
    public void DebugPointUpUI()
    {
        _recipeNameList.Add("焼き鳥");
        _recipePointList.Add(100);
    }
}
