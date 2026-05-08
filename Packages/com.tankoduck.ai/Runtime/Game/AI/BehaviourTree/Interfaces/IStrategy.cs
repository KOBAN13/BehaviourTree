using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Interfaces
{
    public interface IStrategy
    {
        ENodeStatus Process();
        void Reset() {}
    }
}