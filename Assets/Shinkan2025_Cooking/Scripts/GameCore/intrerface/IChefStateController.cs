using System;
using System.Collections;
using System.Collections.Generic;
using Shinkan2025_Cooking.Scripts.GameCore;
using UnityEngine;


public interface IChefStateController
{
    event Action OnEnterShake;
    event Action OnExitShake;
    event Action<float> OnUpdateShake;
    event Action OnEnterThrow;
    event Action OnExitThrow;
    event Action<float> OnUpdateThrow;
    
    void ChefExecuteTrigger(ChefStateTrigger trigger);
}
