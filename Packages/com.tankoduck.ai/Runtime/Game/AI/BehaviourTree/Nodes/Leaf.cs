using System.Linq;
using BehaviourTree;
using Game.AI.BehaviourTree.Actions;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;
using UnityEngine;

namespace Game.AI.BehaviourTree.Nodes
{
    public class Leaf : Node
    {
        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }
        
        private AgentAction _agentAction;
        
        public Leaf(AgentAction agentAction, float priority, string name, IBTDebugger btDebugger) : base(priority, btDebugger)
        {
            _agentAction = agentAction;
            Name = name;
        }

        public override ENodeStatus Process()
        {
            if (!CanStart())
            {
                return Status = ENodeStatus.Failure;
            }

            if (Status != ENodeStatus.Running)
            {
                _agentAction.Start();
            }

            DebugStatus();
            Status = _agentAction.Update(Time.deltaTime);
            return Status;
        }

        public override void Reset()
        {
            _agentAction.Stop();
        }

        private bool CanStart()
        {
            return _agentAction.Precondition.All(b => b.CheckCondition());
        }
    }
}