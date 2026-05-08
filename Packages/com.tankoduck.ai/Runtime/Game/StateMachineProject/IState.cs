namespace Game.StateMachineProject
{
    public interface IState
    {
        IStateMachine StateMachine { get; }
        void OnEnter();
        void OnExit();
        void OnUpdateBehaviour();
        void OnFixedUpdateBehaviour();
        bool TrySwapState();
    }
}