using BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Nodes
{
    public class Inverter : Node
    {
        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }
        
        protected Inverter(string name, int priority, IBTDebugger debugger) : base(priority, debugger)
        {
            Name = name;
        }

        public override ENodeStatus Process()
        {
            Debug(this, Name);

            return Nodes[0].Process() switch
            {
                ENodeStatus.Running => Status = ENodeStatus.Running,
                ENodeStatus.Failure => Status = ENodeStatus.Success,
                _ => Status = ENodeStatus.Failure
            };
        }
    }
}