using System.Collections.Generic;
using Game.AI.BehaviourTree.Enums;
using R3;

namespace Game.AI.BehaviourTree.Debugger
{
    public interface IBTDebugger
    {
        List<string> NameNode { get; }
        List<string> TypeNode { get; }
        ReactiveProperty<ENodeStatus> NodeStatus { get; }

        string GetStatusDebug(ENodeStatus eNodeStatus);

        public List<string> GetNameNode();

        public List<string> GetTypeNode();
    }
}
