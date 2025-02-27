using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// ゲーム終了を通知する機能を提供する
/// </summary>
public interface IGameEndIndicatable
{
    public IObservable<Unit> OnGameEnd {  get; }
}
