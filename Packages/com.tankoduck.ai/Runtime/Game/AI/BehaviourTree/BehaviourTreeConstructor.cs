using System;
using System.Collections.Generic;
using Game.AI.BehaviourTree.Actions;
using Game.AI.BehaviourTree.Beliefs;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Nodes;
using Game.AI.Blackboard;
using Game.AI.Enemies;
using Game.AI.Factory;
using Game.Builder;
using Game.Infrastructure.Helpers.Constants;
using GOAP;

namespace Game.AI.BehaviourTree
{
    public class BehaviourTreeConstructor
    {
        private readonly IBTDebugger _debugger;
        private readonly StrategyFactory _strategyFactory = new();
        private readonly Dictionary<string, AgentAction> _actions = new();
        private readonly Dictionary<string, AgentBelief> _agentBeliefs = new();

        public BehaviourTreeConstructor(IBTDebugger debugger)
        {
            _debugger = debugger;
        }
        
        public BehaviourTreeNode CreateMeleeEnemyBehaviourTree(
            IBlackboardConstructor blackboardConstructor,
            MeleeEnemy meleeEnemy
        )
        { 
            var behaviourTreeNode = new BehaviourTreeNode("Agent Tree", 0, _debugger);
            var blackboardController = blackboardConstructor.GetBlackboardController();
            
            SetupBeliefs();
            SetupActions();
            
            var prioritySelector = new PrioritySelector("PrioritySelector", 0, _debugger);
            
            var moveSequence = new Sequence("Move Sequence", 1, _debugger);
            
            var movement = _actions[NameAgentAction.AgentMoving];
            var moveLeaf = new Leaf(movement, 0f, "Move", _debugger);
            
            moveSequence.AddChild(moveLeaf);
            prioritySelector.AddChild(moveSequence);
            
            
            var attackSequence = new Sequence("Attack Sequence", 2, _debugger);
            
            var attack = _actions[NameAgentAction.AttackingPlayer];
            var attackLeaf = new Leaf(attack, 0f, "Attack", _debugger);
            
            attackSequence.AddChild(attackLeaf);
            prioritySelector.AddChild(attackSequence);
            
            
            var receiveDamageSequence = new Sequence("Receive Damage Sequence", 3, _debugger);
            
            var receiveDamage = _actions[NameAgentAction.ReceiveDamage];
            var receiveDamageLeaf = new Leaf(receiveDamage, 0f, "Receive Damage", _debugger);
            
            receiveDamageSequence.AddChild(receiveDamageLeaf);
            prioritySelector.AddChild(receiveDamageSequence);
            
            
            var dieSequence = new Sequence("Die Sequence", 4, _debugger);
            
            var die = _actions[NameAgentAction.AgentHealthZero]; 
            var dieLeaf = new Leaf(die, 0f, "Die", _debugger);
            
            dieSequence.AddChild(dieLeaf);
            prioritySelector.AddChild(dieSequence);
            
            behaviourTreeNode.AddChild(prioritySelector);
            return behaviourTreeNode;
            
            void SetupActions()
            {
                _actions.Add(NameAgentAction.AgentMoving, new ActionBuilder("Walk")
                    .WithActionStrategy(_strategyFactory.CreateMoveAttack(blackboardController))
                    .WithPrecondition(_agentBeliefs[AgentBeliefsName.AgentMoving])
                    .BuildAgentAction());
            
                _actions.Add(NameAgentAction.AttackingPlayer, new ActionBuilder("PlayerAttack")
                    .WithActionStrategy(_strategyFactory.CreateAttackStrategy(blackboardController))
                    .WithPrecondition(_agentBeliefs[AgentBeliefsName.PlayerToAttackSensor])
                    .BuildAgentAction());
           
                _actions.Add(NameAgentAction.AgentHealthZero, new ActionBuilder("Die")
                    .WithActionStrategy(_strategyFactory.CrateDieStrategy(blackboardController))
                    .WithPrecondition(_agentBeliefs[AgentBeliefsName.AgentHealthZero])
                    .BuildAgentAction());
           
                _actions.Add(NameAgentAction.ReceiveDamage, new ActionBuilder("ReceiveDamage")
                    .WithActionStrategy(_strategyFactory.CreateReceiveDamageStrategy(blackboardController))
                    .WithPrecondition(_agentBeliefs[AgentBeliefsName.PlayerToTakeReceivingDamage])
                    .BuildAgentAction());
            }
            
            void SetupBeliefs()
            {
                var factory = new BeliefFactory(_agentBeliefs);
            
                factory.AddBelief(AgentBeliefsName.AgentMoving, () => true);
                factory.AddBelief(AgentBeliefsName.AgentHealthZero, () => meleeEnemy.GetHealth() <= 0f);
            
                factory.AddSensorBelief(AgentBeliefsName.PlayerToAttackSensor, blackboardController.GetValue<ISensor>(NameSensorPredicate.AttackSensorPredicate));
                factory.AddSensorBelief(AgentBeliefsName.PlayerToTakeReceivingDamage, blackboardController.GetValue<ISensor>(NameSensorPredicate.ReceiveDamageSensorPredicate));
            }
        }
    }
}