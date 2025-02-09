using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFoodPhysicsHandler
{
    public void OnInstantiate(float foodSpeed, float foodLifeTime);
    public void OnStabbed();
}
