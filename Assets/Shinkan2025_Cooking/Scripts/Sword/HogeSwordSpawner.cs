using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using UnityEngine.SocialPlatforms.Impl;

public class HogeSwordSpawner : MonoBehaviour
{
    [SerializeField] Transform _rightControllerPos;
    [SerializeField] GameObject _swordPrefab;

    private void Start()
    {
        InstiniateSword();
    }
    public void InstiniateSword()
    {
        Debug.Log("ã¯ê∂ê¨");
        var sword = Instantiate(_swordPrefab, _rightControllerPos);
        TryGetComponent<ISwordTracker>(out var swordTracker);
        var py =sword.GetComponentInChildren<ISwordPhysicsHandler>();
        swordTracker.SubScribeFoodName(py);

        SubscribeSword(swordTracker, sword);

    }

    public void SubscribeSword(ISwordTracker swordTracker, GameObject sword)
    {
        swordTracker.SwordFullStabbEvent
           .Subscribe(_ =>
           {
               InstiniateSword();

           }).AddTo(sword);
    }
}
