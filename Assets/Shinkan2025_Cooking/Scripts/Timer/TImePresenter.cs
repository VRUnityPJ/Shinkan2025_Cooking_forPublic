using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Timer
{
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
        void Awake()
        {
            Init();
        }
    }
}
