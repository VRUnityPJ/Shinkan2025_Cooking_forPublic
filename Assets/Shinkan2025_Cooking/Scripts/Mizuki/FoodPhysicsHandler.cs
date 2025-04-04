using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Mizuki
{
    public class FoodPhysicsHandler : MonoBehaviour, IFoodPhysicsHandler
    {
        private Rigidbody _rb;
        private float foodLifeTime;
        private bool isStabbed = false;
        private int stabCount = 0;
        [SerializeField] private float foodSpeedPower_x = 0.8f;
        [SerializeField] private float foodSpeedPower_y = 2.0f;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void OnInstantiate(float foodSpeed, float foodLifeTime)
        {
            this.foodLifeTime = foodLifeTime;
            
            if (_rb != null)
            {
                var localPosX = transform.right;
                var localPosY = transform.up;
                var vector = (localPosX * foodSpeedPower_x + localPosY * foodSpeedPower_y) * foodSpeed;
                _rb.AddForce(vector, ForceMode.Impulse);
            }
        }

        void Update()
        {
            foodLifeTime -= Time.deltaTime;

            if (isStabbed == false)
            {
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
    }
}