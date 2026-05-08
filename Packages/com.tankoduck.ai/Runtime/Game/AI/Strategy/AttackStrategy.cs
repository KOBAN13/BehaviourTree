using System;
using Animation;
using Animation.Enums;
using BlackboardScripts;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.AI.Weapon;
using Game.Infrastructure.Helpers.Constants;
using R3;
using UnityEngine;

namespace Game.AI.Strategy
{
    public class AttackStrategy : IActionStrategy
    {
        public bool CanPerform => true;
        public bool Complete { get; private set; }

        private CompositeDisposable _disposable = new();
        private readonly AnimationBrain<EEnemyAnimationType> _animationBrain;
        private readonly AEnemyWeapon _meleeWeapon;
        private readonly Transform _playerTransform;
        private float _clipLength;

        public AttackStrategy(BlackboardController blackboardController)
        {
            _animationBrain = blackboardController.GetValue<AnimationBrain<EEnemyAnimationType>>(BlackboardDataKeys.AnimationBrain);
            _meleeWeapon = blackboardController.GetValue<AEnemyWeapon>(BlackboardDataKeys.AgentMeleeWeapon);
            _playerTransform = blackboardController.GetValue<Transform>(BlackboardDataKeys.PlayerTarget);
        }

        public void Start()
        {
            Complete = false;
            
            _disposable?.Clear();
            
            _disposable = new CompositeDisposable();
            
            _meleeWeapon.InitializeWeapon();
        }
        
        public void Update(float timeDelta)
        {
            if (_meleeWeapon.TryAttack(_playerTransform))
            {
                _animationBrain.PlayAnimation(EEnemyAnimationType.Attack);
                
                Complete = true;
            }
        }

        public void Stop()
        {
            ClearDisposable();
        }
        
        private void ClearDisposable()
        {
            _disposable?.Clear();
            _disposable?.Dispose();
        }
    }
}