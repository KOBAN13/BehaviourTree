using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Policy
{
    public interface IPolicy
    {
        bool ShouldReturn(ENodeStatus node);
    }
}