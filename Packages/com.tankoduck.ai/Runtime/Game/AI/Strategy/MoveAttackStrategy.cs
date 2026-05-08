using Animation;
using Animation.Enums;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.Infrastructure.Helpers.Constants;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Strategy
{
    public class MoveAttackStrategy : IActionStrategy
    {
        public bool CanPerform => !Complete;
        public bool Complete => _agent.remainingDistance <= 1f && _agent.pathPending == false;

        private readonly NavMeshAgent _agent;
        private readonly Transform _playerTransform;
        private readonly AnimationBrain<EEnemyAnimationType> _animationBrain;
        private readonly float _speed;

        public MoveAttackStrategy(BlackboardController blackboardController)
        {
            _agent = blackboardController.GetValue<NavMeshAgent>(BlackboardDataKeys.NavMeshAgent);
            _playerTransform = blackboardController.GetValue<Transform>(BlackboardDataKeys.PlayerTarget);
            _animationBrain = blackboardController.GetValue<AnimationBrain<EEnemyAnimationType>>(BlackboardDataKeys.AnimationBrain);
            _speed = blackboardController.GetValue<float>(BlackboardDataKeys.WalkSpeed);
        }

        public void Start()
        {
            _animationBrain.PlayAnimation(EEnemyAnimationType.Walk, 1f);
            _agent.speed = _speed;
            _agent.SetDestination(_playerTransform.position);
        }

        public void Update(float timeDelta)
        {
            _agent.destination = _playerTransform.position;
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
    }
}