using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Shinkan2025_Cooking.Scripts.Timer
{
    public class StageTimer : MonoBehaviour
    {
        public IObservable<float> OnTimerStart => _onTimerStart;

        [SerializeField] private float _time = 5f;
        [SerializeField] private UnityEvent _timeUpEvent = new();
        private readonly Subject<float> _onTimerStart = new();
        private CancellationToken _token = new();

        private void Start()
        {
            _token = this.GetCancellationTokenOnDestroy();
        }

        [ContextMenu("StartTimer")]
        public void StartTimer()
        {
            StartTimerAsync(_token);
        }

        public async void StartTimerAsync(CancellationToken token)
        {
            _onTimerStart.OnNext(_time);
            await UniTask.Delay(TimeSpan.FromSeconds(_time), cancellationToken: token);
            _timeUpEvent.Invoke();
            Debug.Log("timeゲーム終了");
        }
    }
}