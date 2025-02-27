using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
public interface IGameEndIndicatable
{
    public IObservable<Unit> OnGameEnd {  get; }
}
