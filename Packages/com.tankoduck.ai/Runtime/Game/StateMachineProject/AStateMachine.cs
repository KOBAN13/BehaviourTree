namespace Game.StateMachineProject
{
    public interface IStateMachine
    {
        void TrySwapState<T>() where T : IState;
    }
}