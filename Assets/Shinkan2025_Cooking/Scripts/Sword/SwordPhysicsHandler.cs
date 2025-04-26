using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Sword
{
    public class SwordPhysicsHandler : MonoBehaviour,ISwordPhysicsHandler
    {
        [SerializeField] private List<GameObject> _foodParentPoint;
        [SerializeField] public GameObject _parentSword;

        private IPlayerInputController _playerInputController;

        private Rigidbody _rb;
        private Collider _collider;
        private string _foodName;
        private readonly Subject<Unit> _isStabbed = new();
        private bool _canStabbed;
        private Vector3 _prevPos;

        /// <summary>
        /// 内積の閾値
        /// </summary>
        [SerializeField] private float _stabThreshold = -0.1f;

        public IObservable<Unit> IsStabbed => _isStabbed;
        public string FoodName => _foodName;

        public void Inject(IPlayerInputController playerInputController)
        {
            _playerInputController = playerInputController;
            Debug.Log($"inject={_playerInputController}");
        }

        void Start()
        {
            TryGetComponent(out _rb);
            TryGetComponent(out _collider);

            _prevPos = transform.position;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_canStabbed != true) return;
            if (!other.gameObject.TryGetComponent(out IFoodPhysicsHandler foodPhysicsHandler)) return;
            other.gameObject.TryGetComponent(out Food food);
            if(foodPhysicsHandler.OnStabbed()!=1)return;

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
                    Debug.Log($"{new Vector3(parentLossyScale.x, parentLossyScale.y, parentLossyScale.z)}だよ"+i);

                    foodObj.transform.localScale *= 0.4f;
                    //= new Vector3(parentLossyScale.x * 15, parentLossyScale.y * 15, parentLossyScale.z * 15);

                    _isStabbed.OnNext(Unit.Default);

                    return;
                }
                Debug.Log("枠なし");
            }
        }

        public void OnCompletedFood()
        {
            Debug.Log("串が完成した！");
            /*_parentSword.SetActive(false);
            if (!_parentSword.activeSelf) _parentSword.SetActive(false);*/
            _isStabbed.OnCompleted();
            _isStabbed.Dispose();
            
            Destroy(_parentSword);
        }

        public void Update()
        {
            Vector3 currentPos = transform.position;
            Vector3 velocity = (currentPos - _prevPos) / Time.deltaTime;
            Vector3 moveDicrection = velocity.normalized;
            float dot = Vector3.Dot(transform.up, moveDicrection);

            Debug.Log($"現在の内積{dot}");

            if (dot < _stabThreshold)
            {
                _canStabbed = false;
            }
            else
            {
                _canStabbed = true;
            }
            _prevPos = currentPos;
        }
    }
}
