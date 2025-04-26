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
            Vector3 pos = transform.position;
            Vector3 playerPos = player.transform.position;
            float distance = (new Vector3(playerPos.x, 0, playerPos.z) - new Vector3(pos.x, 0, pos.z)).magnitude;
            if (_rb != null)
            {
                //yのみAddforce
                //FoodSpeedから落ちる時間を計算してなんかの変数に格納
                var localPosX = transform.right;
                var localPosY = transform.up;
                var vector = (new Vector3((playerPos.x - pos.x)*3,  Mathf.Pow(distance/5, 2)* 4, (playerPos.z - pos.z)*3)).normalized * foodSpeed;
                _rb.AddForce(vector, ForceMode.Impulse);
            }
        }

        void Update()
        {
            
            foodLifeTime -= Time.deltaTime;
            _time += Time.deltaTime;
            if (isStabbed == false)
            {
                //XZは少しずつ近づく
                //XZの移動距離は生成された点と終点の距離/落ちるまでの時間
                //FoodMovement();
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
            float vsin = _maxTime * -gravity.y / 2;
            Vector3 playerPos = player.transform.position;
            Vector3 pos = this.transform.position;
            Vector3 dir = (new Vector3(playerPos.x, 0, playerPos.z) - new Vector3(pos.x, 0, pos.z)).normalized;
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            float distance = (new Vector3(playerPos.x, 0, playerPos.z) - new Vector3(pos.x, 0, pos.z)).magnitude;

            if (_time < _maxTime)
            { ;
                x = (playerPos.x - pos.x) * (_time / _maxTime) ;
                z = (playerPos.z - pos.z) * (_time / _maxTime) ;
            }
            y = ((vsin * _time) - (-gravity.y * Mathf.Pow(_time, 2) / 2)) + 1;
            transform.position = new Vector3(x, y, z);
        }
    }
}