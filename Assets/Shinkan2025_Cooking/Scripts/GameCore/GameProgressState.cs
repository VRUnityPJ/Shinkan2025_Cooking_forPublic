using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Shinkan2025_Cooking.Scripts.StateMachine;

namespace Shinkan2025_Cooking.Scripts.GameCore
{

    public class GameProgressStateController : MonoBehaviour
    {
        public IObservable<Unit> OnStartGame => _onStartGame;

        public IObservable<Unit> OnEndGame => _onEndGame;
        
        private Subject<Unit> _onStartGame = new Subject<Unit>();
        
        private Subject<Unit> _onEndGame = new Subject<Unit>();

        public void StartGame()
        {
            _onStartGame.OnNext(Unit.Default);
        }

        public void EndGame()
        {
            _onEndGame.OnNext(Unit.Default);
        }
    }
}
