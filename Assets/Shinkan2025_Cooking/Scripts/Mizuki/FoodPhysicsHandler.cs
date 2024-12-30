using UnityEngine;

public class FoodPhysicsHandler : MonoBehaviour
{
    private Rigidbody _rb;
    private float foodLifeTime;
    private float foodSpeed;
    private bool isStabbed = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnInstantiate(float foodSpeed, float foodLifeTime)
    {
        this.foodSpeed = foodSpeed;
        this.foodLifeTime = foodLifeTime;

        if (_rb != null)
        {
            _rb.velocity = transform.forward * this.foodSpeed;
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

    public void OnStabbed()
    {
        isStabbed = true;
    }
}