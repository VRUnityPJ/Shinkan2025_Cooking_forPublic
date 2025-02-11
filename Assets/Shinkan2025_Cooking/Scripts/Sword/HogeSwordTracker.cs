using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class HogeSwordTracker : MonoBehaviour, ISwordTracker
{
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;

    public void OnStabbed(string name, GameObject foodObj)
    {
        throw new NotImplementedException();
    }

    public void TestDestroy(Transform root)
    {
        throw new NotImplementedException();
    }

    public void OnInject(ISwordPhysicsHandler iswordPysicsHandler)
    {

    }
}
