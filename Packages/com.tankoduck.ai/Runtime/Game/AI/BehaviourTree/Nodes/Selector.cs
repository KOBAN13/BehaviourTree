using BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Nodes
{
    public class Selector : Node
    {
        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }

        public Selector(string name, float priority, IBTDebugger debugger) : base(priority, debugger)
        {
            Name = name;
        }

        public override ENodeStatus Process()
        {
            if (CurrentChild < Nodes.Count)
            {
                switch (Nodes[CurrentChild].Process())
                {
                    case ENodeStatus.Running : 
                        return Status = ENodeStatus.Running;
                    case ENodeStatus.Success : 
                        Reset(); 
                        return Status = ENodeStatus.Success;
                    default:
                        CurrentChild++;
                        return Status = ENodeStatus.Running;
                }
            }
            
            Reset();
            return Status = ENodeStatus.Success;
        }
    }
}