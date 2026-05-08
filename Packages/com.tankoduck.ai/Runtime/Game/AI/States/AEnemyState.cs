using Game.StateMachineProject;

namespace Game.AI.States
{
    public abstract class AEnemyState : IState
    {
        public IStateMachine StateMachine { get; }
        
        public AEnemyState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void OnUpdateBehaviour()
        {
            
        }

        public void OnFixedUpdateBehaviour()
        {
            
        }

        public bool TrySwapState()
        {
            return true;
        }
    }
}