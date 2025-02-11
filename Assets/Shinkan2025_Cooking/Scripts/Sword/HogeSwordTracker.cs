using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class HogeSwordTracker : MonoBehaviour, ISwordTracker
{
    [SerializeField] GameObject[] FoodParentPoint;
    List<string> FoodChildrenName = new();
    private int FoodCounter = 0;
    private const int MaxFoodCounter = 3;
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;

    public void OnStabbed(string name, GameObject foodObj)
    {

    }

    public void TestDestroy(Transform root)
    {

    }

    public void OnInject(ISwordPhysicsHandler swordPysicsHandler)
    {

    }
}
