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
        //�܂�IGameEndIndicatable�̎����悪���܂��Ă��Ȃ��̂ŃR�����g�A�E�g
        //RegisterComponent<T>(builder);
    }

    private void RegisterComponent<T>(IContainerBuilder builder) where T : MonoBehaviour, IGameEndIndicatable
    {
        builder.RegisterComponentInHierarchy<T>().As<IGameEndIndicatable>();
    }
}
