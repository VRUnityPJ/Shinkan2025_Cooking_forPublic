using System.Collections;
using System.Collections.Generic;
using Shinkan2025_Cooking.Scripts.Timer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //まだIGameEndIndicatableの実装先が決まっていないのでコメントアウト
        //RegisterComponent<T>(builder);
    }

    private void RegisterComponent<T>(IContainerBuilder builder) where T : MonoBehaviour, IGameEndIndicatable
    {
        builder.RegisterComponentInHierarchy<T>().As<IGameEndIndicatable>();
    }
}
