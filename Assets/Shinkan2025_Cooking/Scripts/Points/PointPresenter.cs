using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Shinkan2025_Cooking.Scripts.Points;
public class PointPresenter : MonoBehaviour
{
    [SerializeField] PointText _pointText;
    [SerializeField] PointHolder _pointHolder;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _pointHolder.Point
            .Skip(1)
            .Subscribe(currentPoint =>
            {
                _pointText.CountPointText(currentPoint.IntValue.ToString());

             })
            .AddTo(this);
    }
}
