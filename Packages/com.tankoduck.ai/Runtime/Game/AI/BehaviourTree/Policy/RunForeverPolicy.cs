using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Policy
{
    public class RunForeverPolicy : IPolicy
    {
        public bool ShouldReturn(ENodeStatus node) => false;
    }
}