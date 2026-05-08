using System;
using System.Collections.Generic;
using Game.AI.BehaviourTree.Beliefs;
using Game.Builder;
using GOAP;
using UnityEngine;

namespace Game.AI.Factory
{
    public class BeliefFactory
    {
        private readonly Dictionary<string, AgentBelief> _beliefs;

        public BeliefFactory(Dictionary<string, AgentBelief> beliefs)
        {
            _beliefs = beliefs;
        }
        
        public void AddBelief(string nativeKey, Func<bool> condition)
        {
            var belief = new BeliefBuilder(nativeKey);
            _beliefs.Add(nativeKey, 
                belief
                .WithCondition(condition)
                .BuildBelief());
        }

        public void AddSensorBelief(string nativeKey, ISensor sensor)
        {
            var belief = new BeliefBuilder(nativeKey);
            
            _beliefs.Add(nativeKey, 
                belief
                .WithSensor(() => sensor.IsActivate)
                .BuildBelief());
        }
    }
}