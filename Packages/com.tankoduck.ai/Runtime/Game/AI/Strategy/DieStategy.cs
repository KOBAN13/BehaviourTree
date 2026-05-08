using System;
using Animation.Enums;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.AI.Agent;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.AI.Enemies;
using Game.Infrastructure.Helpers;
using Game.Infrastructure.Helpers.Constants;
using UnityEngine;

namespace Game.AI.Strategy
{
    public class DieStrategy : IActionStrategy
    {
        public bool CanPerform => true;
        public bool Complete { get; private set; }

        private readonly AAgentData _agentData;
        private readonly AEnemy _enemy;
        private readonly Renderer[] _renderers;
        private readonly MaterialPropertyBlock _propertyBlock = new();

        private Tween _deathTween;

        public DieStrategy(BlackboardController blackboardController)
        {
            _renderers = blackboardController.GetValue<Renderer[]>(BlackboardDataKeys.AgentRenderer);
            _enemy = blackboardController.GetValue<AEnemy>(BlackboardDataKeys.EnemyInstance);
            _agentData = blackboardController.GetValue<AAgentData>(BlackboardDataKeys.AgentData);
        }

        public async void Start()
        {
            Complete = false;
            
            _agentData.NavMeshAgent.isStopped = true;
            _agentData.NavMeshAgent.ResetPath();
            _agentData.Collider.enabled = false;

            _agentData.AnimationBrain.PlayAnimation(EEnemyAnimationType.Die);
            
            var enemyDieAnimationData = _agentData.EnemyDieAnimationData;

            await UniTask.Delay(TimeSpan.FromSeconds(enemyDieAnimationData.DelayToStartAnimation));
            
            _deathTween?.Kill();

            _deathTween = DOTween.To(() => 0f, value =>
                    {
                        foreach (var renderer in _renderers)
                        {
                            renderer.GetPropertyBlock(_propertyBlock);
                            _propertyBlock.SetFloat(ShaderId.DissolveHash, value);
                            renderer.SetPropertyBlock(_propertyBlock);
                        }
                    },
                    1.1f,
                    enemyDieAnimationData.AnimationDuration)
                .SetEase(enemyDieAnimationData.ScaleEase)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    Complete = true;
                    _enemy.OnDeactivate();
                });
        }
    }
}