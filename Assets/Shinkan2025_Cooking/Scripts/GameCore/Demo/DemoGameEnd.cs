using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using VContainer;
using System.Threading;
using Cysharp.Threading.Tasks;

public class DemoGameEnd : MonoBehaviour, IGameEndIndicatable
{
    private Subject<Unit> _onGameEnd = new();

    public IObservable<Unit> OnGameEnd => _onGameEnd;
    private CancellationToken _token = new();
    [SerializeField] private float _waittime = 4f;

    [ContextMenu("GameEnd")]
    public void FinishGame()
    {
        _token = this.GetCancellationTokenOnDestroy();
        EndGameAsync(_token);
    }
    public async void EndGameAsync(CancellationToken token)
    {
        
        await UniTask.Delay(TimeSpan.FromSeconds(_waittime), cancellationToken: token);
        _onGameEnd.OnNext(Unit.Default);
        _onGameEnd.OnCompleted();

        _onGameEnd.Dispose();
        Debug.Log("ゲーム終了");
    }
}
