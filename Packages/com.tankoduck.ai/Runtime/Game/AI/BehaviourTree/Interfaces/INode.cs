using System.Collections.Generic;
using Game.AI.BehaviourTree.Enums;

namespace Game.AI.BehaviourTree.Interfaces
{
    public interface INode
    {
        ENodeStatus Status { get; }
        IReadOnlyList<INode> Nodes { get; }
        string Name { get; }
        float Priority { get; }
        int CurrentChild { get; }
        ENodeStatus Process();
        void Reset();
        void AddChild(INode node);
    }
}