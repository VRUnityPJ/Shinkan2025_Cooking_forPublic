using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// ゲーム終了を通知する機能を提供する
/// </summary>
public interface IGameProgressIndicatable
{
    public IObservable<Unit> OnStartGame { get; }
    public IObservable<Unit> OnEndGame {  get; }

    void StartGame();
    void EndGame();
}
