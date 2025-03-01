using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IRecipeObservable
{
    public IReadOnlyReactiveProperty<string> FinishedRecipeName { get; }
}
