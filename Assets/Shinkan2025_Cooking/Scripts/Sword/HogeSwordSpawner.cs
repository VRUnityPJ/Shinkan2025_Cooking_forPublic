using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogeSwordSpawner : MonoBehaviour, ISwordSpawnable
{
    [SerializeField] Transform _rightControllerPos;
    [SerializeField] GameObject _swordPrefab;
    public ISwordTracker InstiniateSword()
    {
        var sword = Instantiate(_swordPrefab, _rightControllerPos);
        throw new System.NotImplementedException();
    }
}
