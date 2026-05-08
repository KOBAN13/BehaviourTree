using BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Nodes
{
    public class Sequence : Node
    {
        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }

        public Sequence(string name, float priority, IBTDebugger debugger) : base(priority, debugger)
        {
            Name = name;
        }

        public override ENodeStatus Process()
        {
            if (CurrentChild < Nodes.Count)
            {
                switch (Nodes[CurrentChild].Process())
                {
                    case ENodeStatus.Running: 
                        return Status = ENodeStatus.Running;
                    case ENodeStatus.Failure:
                        Reset();
                        return Status = ENodeStatus.Failure;
                    default: 
                        CurrentChild++;
                        return Status = CurrentChild == Nodes.Count ? ENodeStatus.Success : ENodeStatus.Running;
                }
            }
            
            Reset();
            return Status = ENodeStatus.Success;
        }
    }
}