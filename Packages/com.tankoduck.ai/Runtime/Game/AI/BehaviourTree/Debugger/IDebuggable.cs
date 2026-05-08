namespace Game.AI.BehaviourTree.Debugger
{
    public interface IDebuggable
    {
        IBTDebugger Debugger { get; }
    }
}