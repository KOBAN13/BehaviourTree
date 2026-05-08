using System;
using System.Collections.Generic;
using System.Linq;
using Game.StateMachineProject;
using VContainer.Unity;

namespace Game.AI.States
{
    public class EnemyStateMachine : ITickable, IInitializable, IDisposable, IFixedTickable, IStateMachine
    {
        private List<IState> _states;
        private StateMachine _stateMachine;
        private EnemyStateMachineData _stateMachineData;
        private readonly List<IDisposable> _disposables = new();

        public EnemyStateMachine(EnemyStateMachineData stateMachineData)
        {
            _stateMachineData = stateMachineData;
        }

        public void AddDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
        
        public EnemyStateMachineData GetStateMachineData() => _stateMachineData;
        public StateMachine GetStateMachine() => _stateMachine;

        public void TrySwapState<T>() where T : IState
        {
            if (_stateMachine.CurrentStates is T) return;
            
            var state = _stateMachine.States.Values.FirstOrDefault(typeState => typeState is T);

            if (state == null) throw new ArgumentNullException("Dont find state");

            if (state.TrySwapState())
            {
                _stateMachine.SwitchStates<T>();
            }
        }

        public void Tick()
        {
            _stateMachine.CurrentStates.OnUpdateBehaviour();
        }
        
        public void FixedTick()
        {
            _stateMachine.CurrentStates.OnFixedUpdateBehaviour();
        }
        
        public void Initialize()
        {
            _states = new()
            {
                new EnemyRespawn(this),
                new EnemyLife(this),
                new EnemyDie(this)
            };
            
            _stateMachine = new StateMachine(_states);
            
            _stateMachine.SwitchStates<EnemyRespawn>();
        }

        public void Dispose()
        {
            foreach (var dispose in _disposables)
            {
                dispose.Dispose();
            }
            _stateMachineData.Dispose();
        }
    }
}