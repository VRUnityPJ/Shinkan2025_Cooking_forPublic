using System.Collections;
using System.Collections.Generic;
using Shinkan2025_Cooking.Scripts.Timer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private GameProgress _gameProgress;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterCompenentInHierarchy<DemoGameEnd>(builder);

        //Inject先のクラスのインスタンス登録
        builder.RegisterComponent(_gameProgress);
    }

    /// <summary>
    /// IGameEndIndicatableがどのクラスで実装するか未定のため、
    /// どのクラスでも対応できるようにジェネリックにしてある
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    private void RegisterCompenentInHierarchy<T>(IContainerBuilder builder) where T : MonoBehaviour, IGameEndIndicatable
    {
        builder.RegisterComponentInHierarchy<T>()
          .As<IGameEndIndicatable>();
    }
}
