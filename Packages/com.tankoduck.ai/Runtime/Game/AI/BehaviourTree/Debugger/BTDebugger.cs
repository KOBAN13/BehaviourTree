using System.Collections.Generic;
using System.Linq;
using Game.AI.BehaviourTree.Enums;
using R3;

namespace Game.AI.BehaviourTree.Debugger
{
    public class BTDebugger : IBTDebugger
    {
        public List<string> NameNode { get; private set; } = new();
        public List<string> TypeNode { get; private set; } = new();
        public ReactiveProperty<ENodeStatus> NodeStatus { get; private set; } = new();

        private CompositeDisposable _compositeDisposable = new();


        public string GetStatusDebug(ENodeStatus eNodeStatus)
        {
            return $"{eNodeStatus}";
        }

        public List<string> GetNameNode()
        {
            return NameNode.ToList();
        }

        public List<string> GetTypeNode()
        {
            return TypeNode.ToList();
        }
    }
}
