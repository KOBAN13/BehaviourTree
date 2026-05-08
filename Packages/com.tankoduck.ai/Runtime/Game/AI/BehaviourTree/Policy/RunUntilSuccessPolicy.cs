using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Policy
{
    public class RunUntilSuccessPolicy : IPolicy
    {
        public bool ShouldReturn(ENodeStatus node) => node == ENodeStatus.Success;
    }
}