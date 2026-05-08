using System.Collections.Generic;
using System.Linq;
using BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;
using Game.AI.BehaviourTree.Interfaces;

namespace Game.AI.BehaviourTree.Nodes
{
    public class PrioritySelector : Node
    {
        private List<INode> _sortedNodes;
        private IReadOnlyList<INode> SortedNodes => _sortedNodes ??= SortNodes();

        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }
        
        public PrioritySelector(string name, float priority, IBTDebugger debugger)
            : base(priority, debugger)
        {
            Name = name;
        }
        
        protected virtual List<INode> SortNodes()
        {
            return Nodes.OrderByDescending(x => x.Priority).ToList();
        }

        public override void Reset()
        {
            base.Reset();
            _sortedNodes = null;
        }

        public override ENodeStatus Process()
        {
            foreach (var node in SortedNodes)
            {
                switch (node.Process())
                {
                    case ENodeStatus.Running:
                        return ENodeStatus.Running;
                    case ENodeStatus.Success:
                        Reset();
                        return ENodeStatus.Success;
                    case ENodeStatus.Failure:
                        continue;
                }
            }
            
            Reset();
            
            return ENodeStatus.Failure;
        }
    }
}
