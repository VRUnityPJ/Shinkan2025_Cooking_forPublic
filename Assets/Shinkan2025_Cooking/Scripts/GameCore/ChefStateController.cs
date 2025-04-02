using System;
using UnityEngine;
using Shinkan2025_Cooking.Scripts.StateMachine;

namespace Shinkan2025_Cooking.Scripts.GameCore
{
    public enum ChefState
    {
        Shake,
        Throw
    }

    public enum ChefStateTrigger
    {
        ToShake,
        ToThrow
    }
    public class ChefStateController : MonoBehaviour
    {
        public event Action OnEnterShake;
        public event Action OnExitShake;
        public event Action<float> OnUpdateShake;
        public event Action OnEnterThrow;
        public event Action OnExitThrow;
        public event Action<float> OnUpdateThrow;
        
        [SerializeField] private ChefState _initialChefState = ChefState.Shake;
        
        private StateMachine<ChefState, ChefStateTrigger> _chefStateMachine;
        public StateMachine<ChefState, ChefStateTrigger> chefStateMachine => _chefStateMachine;

        void Start()
        {
            StateInitialize();
        }

        void Update()
        {
            _chefStateMachine.Update(Time.deltaTime);
        }

        private void StateInitialize()
        {
            _chefStateMachine = new StateMachine<ChefState, ChefStateTrigger>(_initialChefState);
            
            _chefStateMachine.SetupState
            (
                ChefState.Shake,
                () => OnEnterShake?.Invoke(),
                () => OnExitShake?.Invoke(),
                deltaTime => OnUpdateShake?.Invoke(Time.deltaTime)
            );
            
            _chefStateMachine.SetupState
            (
                ChefState.Throw,
                () => OnEnterThrow?.Invoke(),
                () => OnExitThrow?.Invoke(),
                deltaTime => OnUpdateThrow?.Invoke(Time.deltaTime)
            );
            
            _chefStateMachine.AddTransition
            (
                ChefState.Shake,
                ChefState.Throw,
                ChefStateTrigger.ToThrow
            );

            _chefStateMachine.AddTransition
            (
                ChefState.Throw,
                ChefState.Shake,
                ChefStateTrigger.ToShake
            );
        }

        public void ChefExecuteTrigger(ChefStateTrigger trigger)
        {
            _chefStateMachine.ExecuteTrigger(trigger);
        }
    }


}
