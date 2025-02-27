using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Shinkan2025_Cooking.Scripts.Timer;
using Cysharp.Threading.Tasks;
public class TimePresenter : MonoBehaviour
{
    [SerializeField] TimerText _timerText;
    [SerializeField] StageTimer _stageTimer;

    private void Init()
    {
        _stageTimer.OnTimerStart
            .Subscribe(x =>
            {
                _timerText.CountDownTimeText(x).Forget();
            }).AddTo(this);
    }
    void Start()
    {
        Init();
    }

}
