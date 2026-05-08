using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.Infrastructure.Helpers.Constants;
using GOAP;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Strategy
{
    public class ReceiveDamageNode : IActionStrategy
    {
        public bool CanPerform => true;
        public bool Complete { get; private set; }

        private readonly Transform _transform;
        private readonly ReceiveDamageSensor _receiveDamageSensor;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _knockbackDistance;
        private readonly float _knockbackTime;
        private readonly float _recoveryTime;

        public ReceiveDamageNode(BlackboardController blackboardController)
        {
            _transform = blackboardController.GetValue<Transform>(BlackboardDataKeys.AgentTransform);
            _receiveDamageSensor = (ReceiveDamageSensor)blackboardController.GetValue<ISensor>(NameSensorPredicate.ReceiveDamageSensorPredicate);
            _navMeshAgent = blackboardController.GetValue<NavMeshAgent>(BlackboardDataKeys.NavMeshAgent);
            _knockbackDistance = blackboardController.GetValue<float>(BlackboardDataKeys.KnockbackDistance);
            _knockbackTime = blackboardController.GetValue<float>(BlackboardDataKeys.KnockbackTime);
            _recoveryTime = blackboardController.GetValue<float>(BlackboardDataKeys.RecoveryTime);
        }

        public void Start()
        {
            Complete = false;
            
            var direction = _receiveDamageSensor.ImpactDirection;
            direction.y = 0;

            _transform.DOMove(_transform.position + direction.normalized * _knockbackDistance, _knockbackTime)
                .OnComplete(Recover);
        }

        public void Stop()
        {
            _receiveDamageSensor.OnDeactivateSensor();
        }
        
        private void Recover()
        {
            _navMeshAgent.isStopped = true;
            UniTask.Delay(TimeSpan.FromSeconds(_recoveryTime))
                .ContinueWith(() =>
                {
                    _navMeshAgent.isStopped = false;
                    Complete = true;
                })
                .Forget();
        }
    }
}