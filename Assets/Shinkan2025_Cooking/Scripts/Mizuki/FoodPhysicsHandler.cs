using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Mizuki
{
    public class FoodPhysicsHandler : MonoBehaviour, IFoodPhysicsHandler
    {
        [SerializeField] private float foodSpeedPower_x = 0.8f;
        [SerializeField] private float foodSpeedPower_y = 2.0f;
        private Rigidbody _rb;
        private Vector3 gravity = Physics.gravity;
        private GameObject player;
        private float foodLifeTime;
        private bool isStabbed = false;
        private int stabCount = 0;
        private float _time = 0;
        private float _maxTime = 4;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void OnInstantiate(float foodSpeed, float foodLifeTime)
        {
            this.foodLifeTime = foodLifeTime;
            
            if (_rb != null)
            {
                //yのみAddforce
                //FoodSpeedから落ちる時間を計算してなんかの変数に格納
                /*var localPosX = transform.right;
                var localPosY = transform.up;
                var vector = (localPosX * foodSpeedPower_x + localPosY * foodSpeedPower_y) * foodSpeed;
                _rb.AddForce(vector, ForceMode.Impulse);*/
            }
        }

        void Update()
        {
            
            foodLifeTime -= Time.deltaTime;

            if (isStabbed == false)
            {
                //XZは少しずつ近づく
                //XZの移動距離は生成された点と終点の距離/落ちるまでの時間
                FoodMovement();
                if (foodLifeTime <= 0)
                {
                    OnTimeOut();
                }
            }
        }

        private void OnTimeOut()
        {
            Destroy(gameObject);
        }

        public int OnStabbed()
        {
            stabCount++;
            isStabbed = true;
            _rb.isKinematic = true;
            return stabCount;
        }

        private void FoodMovement()
        {
            float vsin = _maxTime * gravity.y / 2;
            Vector3 playerPos = player.transform.position;
            Vector3 pos = this.transform.position;
            Vector3 dir = (playerPos - pos).normalized;
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            float distance = (playerPos - pos).magnitude;
            
            if (_time > _maxTime) return;
            
            _time += Time.deltaTime;
            x = distance * (_time / _maxTime) * dir.x;
            y = vsin * (_time / _maxTime) - gravity.y * Mathf.Pow(_time / _maxTime, 2) / 2;
            z = distance * (_time / _maxTime) * dir.z;
            transform.position = new Vector3(x, y, z);
        }
    }
}