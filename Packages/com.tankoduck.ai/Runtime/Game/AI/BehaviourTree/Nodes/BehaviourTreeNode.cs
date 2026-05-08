using System.Text;
using BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Enums;
using Game.AI.BehaviourTree.Policy;

namespace Game.AI.BehaviourTree.Nodes
{
    public class BehaviourTreeNode : Node
    {
        public override ENodeStatus Status { get; protected set; }
        public sealed override string Name { get; protected set; }
        
        private readonly IPolicy _policy;

        public BehaviourTreeNode(string name, float priority, IBTDebugger debugger, IPolicy policy = null) : base(priority, debugger)
        {
            Name = name;
            _policy = policy ?? Policies.RunForeverPolicy;
        }

        public override ENodeStatus Process()
        {
            var status = Nodes[CurrentChild].Process();

            if (_policy.ShouldReturn(status))
            {
                return Status = status;
            }
            
            CurrentChild = (CurrentChild + 1) % Nodes.Count;

            return Status = ENodeStatus.Success;
        }
        
        public void PrintTree() 
        {
            StringBuilder sb = new StringBuilder();
            PrintNode(this, 0, sb);
            UnityEngine.Debug.Log(sb.ToString());
        }

        private static void PrintNode(Node node, int indentLevel, StringBuilder sb)
        {
            sb.Append(' ', indentLevel * 2).AppendLine(node.Name);
            foreach (var node1 in node.Nodes)
            {
                var child = (Node)node1;
                PrintNode(child, indentLevel + 1, sb);
            }
        }
    }
}