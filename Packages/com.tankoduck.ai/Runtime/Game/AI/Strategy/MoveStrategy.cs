using System;
using Animation;
using Animation.Enums;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.Infrastructure.Helpers.Constants;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Strategy
{
    public class MoveStrategy : IActionStrategy
    {
        public bool CanPerform => !Complete;
        public bool Complete => _agent.remainingDistance <= 1f && !_agent.pathPending;
        
        private readonly NavMeshAgent _agent;
        private readonly Func<Vector3> _destination;
        private readonly bool _isUpdate;
        private readonly AnimationBrain<EEnemyAnimationType> _animationBrain;

        public MoveStrategy(BlackboardController blackboardController, Func<Vector3> destination)
        {
            _agent = blackboardController.GetValue<NavMeshAgent>(BlackboardDataKeys.NavMeshAgent);
            _animationBrain = blackboardController.GetValue<AnimationBrain<EEnemyAnimationType>>(BlackboardDataKeys.AnimationBrain);
            _destination = destination;
        }

        public void Start()
        {
            _animationBrain.PlayAnimation(EEnemyAnimationType.Walk, 0.2f);
            _agent.SetDestination(_destination());
        }
        
        public void Stop() => _agent.ResetPath();
    }
}