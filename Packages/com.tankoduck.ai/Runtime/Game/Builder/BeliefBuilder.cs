using System;
using Game.AI.BehaviourTree.Beliefs;
using UnityEngine;

namespace Game.Builder
{
    public class BeliefBuilder
    {
        private AgentBelief _agentBelief;

        public BeliefBuilder(string name)
        {
            _agentBelief = new AgentBelief(name);
        }

        public BeliefBuilder WithCondition(Func<bool>  condition)
        {
            _agentBelief.SetCondition(condition);
            return this;
        }
        
        public BeliefBuilder WithSensor(Func<bool> pointer)
        {
            _agentBelief.SetCondition(pointer);
            return this;
        }

        public AgentBelief BuildBelief()
        {
            return _agentBelief;
        }
    }
}