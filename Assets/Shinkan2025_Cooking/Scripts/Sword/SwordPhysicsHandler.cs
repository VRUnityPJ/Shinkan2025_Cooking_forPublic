using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class SwordPhysicsHandler : MonoBehaviour,ISwordPhysicsHandler
{
    [SerializeField] private List<GameObject> _foodParentPoint;
    [SerializeField] GameObject _parentSword;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private string _foodName;
    private readonly Subject<Unit> _isStabbed = new();
    public IObservable<Unit> IsStabbed => _isStabbed;
    public string FoodName => _foodName;

    void Start()
    {
        TryGetComponent(out _rigidbody);
        TryGetComponent(out _collider);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out IFoodPhysicsHandler foodPhysicsHandler)) return;
        other.gameObject.TryGetComponent(out Food food);
        foodPhysicsHandler.OnStabbed();
        OnStabbed(food.GetName(), other.gameObject);

    }

    public void OnStabbed(string name, GameObject foodObj)
    {
        _foodName = name;
        for (int i = 0; i < _foodParentPoint.Count; i++)
        {
            if (_foodParentPoint[i].transform.childCount==0)
            {
                Debug.Log($"要素数{i}");
                foodObj.transform.parent = _foodParentPoint[i].transform;
                foodObj.transform.position = _foodParentPoint[i].transform.position;

                //刺さる食材の大きさを適切なサイズに変更
                var localScale = foodObj.transform.localScale;
                var parentLossyScale = _foodParentPoint[i].transform.localScale;
                Debug.Log($"{new Vector3(parentLossyScale.x, parentLossyScale.y, parentLossyScale.z)}だよ");

                foodObj.transform.localScale
                    = new Vector3(parentLossyScale.x * 15, parentLossyScale.y * 15, parentLossyScale.z * 15);

                _isStabbed.OnNext(Unit.Default);

                break;
            }
        }
    }

    public void OnCompletedFood()
    {
        Debug.Log("串が完成した！");
        _isStabbed.OnCompleted();
        _isStabbed.Dispose();
        _parentSword.SetActive(false);
        //Destroy(_parentSword);
    }
}
