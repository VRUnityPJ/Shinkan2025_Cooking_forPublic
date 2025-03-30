using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Mizuki
{
    public class FoodPhysicsHandler : MonoBehaviour, IFoodPhysicsHandler
    {
        private Rigidbody _rb;
        private float foodLifeTime;
        private bool isStabbed = false;
        private int stabCount = 0;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void OnInstantiate(float foodSpeed, float foodLifeTime)
        {
            this.foodLifeTime = foodLifeTime;

            if (_rb != null)
            {
                _rb.AddForce(transform.InverseTransformPoint(new Vector3(0, 30, 40) * foodSpeed));
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