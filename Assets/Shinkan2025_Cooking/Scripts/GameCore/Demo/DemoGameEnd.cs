using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using VContainer;

public class DemoGameEnd : MonoBehaviour, IGameEndIndicatable
{
    private Subject<Unit> _onGameEnd = new();

    public IObservable<Unit> OnGameEnd => _onGameEnd;

    [ContextMenu("GameEnd")]
    public void FinishGame()
    {
        _onGameEnd.OnNext(Unit.Default);
        _onGameEnd.OnCompleted();

        _onGameEnd.Dispose();
    }
}
