using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Policy
{
    public class RunUntilFailPolicy : IPolicy
    {
        public bool ShouldReturn(ENodeStatus node) => node == ENodeStatus.Failure;
    }
}