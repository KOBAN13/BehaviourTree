using System;
using System.Collections.Generic;
using BlackboardScripts;
using Game.AI.BehaviourTree.Beliefs;
using Game.AI.BehaviourTree.Enums;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.Infrastructure.Helpers;

namespace Game.AI.BehaviourTree.Actions
{
    public struct AgentAction : IEquatable<AgentAction>, IComparable<AgentAction>
    {
        public string Name { get; private set; }
        
        private ENodeStatus _status;
        
        public readonly HashSet<AgentBelief> Precondition;

        private IActionStrategy _actionStrategy;

        public AgentAction(string name)
        {
            Name = name;
            
            Precondition = new HashSet<AgentBelief>();
            _status = ENodeStatus.Success;
            _actionStrategy = null;
        }
        

        public void SetActionStrategy(IActionStrategy strategy)
        {
            Preconditions.CheckNotNull(strategy);
            _actionStrategy = strategy;
        }
        
        public bool Complete => _actionStrategy.Complete;
        public void Start() => _actionStrategy.Start();

        public ENodeStatus Update(float deltaTime)
        { 
            if (_actionStrategy.CanPerform)
            { 
                _actionStrategy.Update(deltaTime);
                _status = ENodeStatus.Running;
            }
            
            if(_actionStrategy.Complete == false) 
                return _status = ENodeStatus.Running;
            
            _status = ENodeStatus.Success;
            return _status;
        }

        public void Stop() => _actionStrategy.Stop();
        
        #region IEquatableAndIComparable
        public int CompareTo(AgentAction other) => string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        
        public static bool operator >(AgentAction a, AgentAction b) => a.CompareTo(b) > 0;
        public static bool operator <(AgentAction a, AgentAction b) => a.CompareTo(b) < 0;
        public static bool operator >=(AgentAction a, AgentAction b) => a.CompareTo(b) >= 0;
        public static bool operator <=(AgentAction a, AgentAction b) => a.CompareTo(b) <= 0;
        
        public bool Equals(AgentAction other) => Name == other.Name;

        public override bool Equals(object obj) => obj is AgentAction other && Equals(other);

        public override int GetHashCode() => Name.ComputeHash();

        public static bool operator ==(AgentAction a, AgentAction b) => a.Equals(b);
        public static bool operator !=(AgentAction a, AgentAction b) => !a.Equals(b);

        #endregion
    }
}
