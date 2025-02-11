using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

public class HogeSwordSpawner : MonoBehaviour, ISwordSpawnable
{
    [SerializeField] Transform _rightControllerPos;
    [SerializeField] GameObject _swordPrefab;

    private void Start()
    {
        InstiniateSword();
    }
    public ISwordTracker InstiniateSword()
    {
        Debug.Log("ã¯ê∂ê¨");
        var sword = Instantiate(_swordPrefab, _rightControllerPos);
        sword.TryGetComponent<ISwordTracker>(out var swordTracker);

        swordTracker.SwordFullStabbEvent
            .Subscribe(_ =>
            {
                InstiniateSword();

            }).AddTo(sword);

        throw new System.NotImplementedException();
    }

}
