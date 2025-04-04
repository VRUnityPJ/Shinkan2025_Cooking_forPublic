using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using Shinkan2025_Cooking.Scripts.StateMachine;

namespace Shinkan2025_Cooking.Scripts.GameCore
{

    public class GameProgressStateController : MonoBehaviour, IGameProgressIndicatable
    {
        public IObservable<Unit> OnStartGame => _onStartGame;

        public IObservable<Unit> OnEndGame => _onEndGame;
        
        private Subject<Unit> _onStartGame = new Subject<Unit>();
        
        private Subject<Unit> _onEndGame = new Subject<Unit>();
        private CancellationToken _token = new();
        [SerializeField] private float _time = 5f;

        public void StartGame()
        {
            _onStartGame.OnNext(Unit.Default);
            _onStartGame.OnCompleted();
            _token = this.GetCancellationTokenOnDestroy();
        }

        public void EndGame()
        {
            EndGameAsync(_token);
        }
        public async void EndGameAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_time), cancellationToken: token);
            _onEndGame.OnNext(Unit.Default);
            _onEndGame.OnCompleted();
            Debug.Log("ゲーム終了");
        }
    }
}
