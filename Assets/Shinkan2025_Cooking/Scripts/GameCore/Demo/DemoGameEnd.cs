using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using VContainer;

/*public class DemoGameEnd : MonoBehaviour, IGameProgressIndicatable
{
    private Subject<Unit> _onStartGame = new();
    private Subject<Unit> _onEndGame = new();

    public IObservable<Unit> OnStartGame => _onStartGame;
    public IObservable<Unit> OnEndGame => _onEndGame;

    [ContextMenu("EndGame")]
    public void FinishGame()
    {
        _onEndGame.OnNext(Unit.Default);
        _onEndGame.OnCompleted();

        _onEndGame.Dispose();
    }
}*/
