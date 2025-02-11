using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SwordPhysicsHandler : MonoBehaviour,ISwordPhysicsHandler
{
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    private ISwordSpawnable _swordSpawner;
    private ISwordTracker _swordTracker;
    private Collider _collider;
    void Start()
    {
        TryGetComponent(out _rigidbody);
        TryGetComponent(out _swordSpawner);
        // TryGetComponent(out _collider);

        SwordSetting();
    }

   private void OnTriggerEnter(Collider other)
    {
        Debug.Log("あああああああああああああ");
        //foodクラスがないので仮で物理のやつを取得してどうにかする
        if (!other.gameObject.TryGetComponent(out IFoodPhysicsHandler foodPhysicsHandler)) return;
        //仮でX
        _swordTracker.OnStabbed("x",other.gameObject);
        foodPhysicsHandler.OnStabbed();
    }

    private void OnFullStabbedSword()
    {
        // _swordTracker.TestDestroy();
        SwordSetting();
    }
    private void SwordSetting()
    {
        Debug.Log("swordSetting");
        _swordTracker = _swordSpawner.InstiniateSword();
        _swordTracker.SwordFullStabbEvent
            .Subscribe(inputValue => OnFullStabbedSword())
            .AddTo(this);
    }
}
