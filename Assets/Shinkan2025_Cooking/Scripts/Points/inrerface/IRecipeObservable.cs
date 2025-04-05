using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public interface IRecipeObservable
{
    public IObservable<string> RecipeName { get; }

}
