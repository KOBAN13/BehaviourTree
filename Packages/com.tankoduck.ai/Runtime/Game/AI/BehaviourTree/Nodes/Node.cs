using System.Collections.Generic;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;
using Game.AI.BehaviourTree.Interfaces;

namespace BehaviourTree
{
    public abstract class Node : INode, IDebuggable
    {
        public abstract string Name { get; protected set; }
        public abstract ENodeStatus Status { get; protected set; }
        public int CurrentChild { get; protected set; }
        public IBTDebugger Debugger { get; }
        public float Priority { get; }
        public IReadOnlyList<INode> Nodes => _nodes;
        
        private readonly List<INode> _nodes = new();

        public virtual ENodeStatus Process()
        {
            Status = Nodes[CurrentChild].Process();
            return Status;
        }
        
        public virtual void Reset()
        {
            CurrentChild = 0;
            
            foreach (var node in _nodes)
                node.Reset();
            
            DebugReset();
        }
        
        public void AddChild(INode node) => _nodes.Add(node);

        private void DebugReset()
        {
            Debugger.NodeStatus.Value = Status;
            Debugger.NameNode.Clear();
            Debugger.TypeNode.Clear();
        }

        protected void Debug<T>(T node, string nameNode)
        {
            Debugger.NameNode.Add(nameNode);
            Debugger.TypeNode.Add(node.GetType().ToString());
        }

        protected void DebugStatus()
        {
            Debugger.NodeStatus.Value = Status;
        }

        protected Node(float priority, IBTDebugger debugger)
        {
            Priority = priority;
            Debugger = debugger;
        }
    }
}
