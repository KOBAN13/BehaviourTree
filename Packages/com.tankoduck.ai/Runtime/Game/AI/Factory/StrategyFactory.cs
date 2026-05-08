using System;
using BlackboardScripts;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.AI.Strategy;
using GOAP;
using UnityEngine;

namespace Game.AI.Factory
{
    public class StrategyFactory
    {
        public IActionStrategy CreateIdleStrategy(BlackboardController blackboardController) =>
            new IdleStrategy(blackboardController);

        public IActionStrategy CreateMoveToPointStrategy(BlackboardController blackboardController, Func<Vector3> destination) 
            => new MoveStrategy(blackboardController, destination);
        
        public IActionStrategy CreateMoveAttack(BlackboardController blackboardController) 
            => new MoveAttackStrategy(blackboardController);
        
        public IActionStrategy CreateAttackStrategy(BlackboardController blackboardController)
            => new AttackStrategy(blackboardController);
        
        public IActionStrategy CrateDieStrategy(BlackboardController blackboardController) 
            => new DieStrategy(blackboardController);
        
        public IActionStrategy CreateReceiveDamageStrategy(BlackboardController blackboardController)
            => new ReceiveDamageNode(blackboardController);
    }
}