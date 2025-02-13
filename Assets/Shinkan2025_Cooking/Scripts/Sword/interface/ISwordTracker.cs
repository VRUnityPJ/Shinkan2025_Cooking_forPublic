using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface ISwordTracker
{
    public IObservable<Unit> SwordFullStabbEvent{get;}
    public void OnStabbed(string name,GameObject foodObj);
    public void TestDestroy(Transform root);
    public void SubScribeFoodName(ISwordPhysicsHandler swordPhysicsHandler);

}
