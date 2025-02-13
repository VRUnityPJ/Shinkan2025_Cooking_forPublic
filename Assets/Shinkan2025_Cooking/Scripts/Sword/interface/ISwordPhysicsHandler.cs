using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface ISwordPhysicsHandler
{
    public IObservable<Unit> IsStabbed {  get; }
    public string FoodName { get; }
    public void OnCompletedFood();
}
