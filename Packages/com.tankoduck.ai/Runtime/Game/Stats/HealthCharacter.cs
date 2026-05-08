using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Infrastructure.Helpers;
using Game.Stats.Impl;
using Game.Stats.Interface;
using R3;
using UnityEngine;

namespace Game.Stats
{
    public class HealthCharacter : IHealthStats, ICharacterStatConfig<HealthConfig>, IDisposable
    {
        public ECharacterStat StatType => ECharacterStat.Health;
        public float MaxValue { get; private set; }
        public Observable<float> OnCurrentValueChanged => _currentHealth;
        public ReadOnlyReactiveProperty<float> CurrentHealthPercentage => _amountHealthPercentage;
        public float CurrentHealth => _currentHealth.Value;

        private CancellationTokenSource _cancellationTokenSource;
        private HealthConfig _config;
        
        private readonly ReactiveProperty<float> _currentHealth = new();
        private readonly ReactiveProperty<float> _amountHealthPercentage = new();
        private readonly OperationWithHealth _operationWithHealth;
        
        public HealthCharacter(OperationWithHealth operationWithHealth = null)
        {
            _operationWithHealth = operationWithHealth;
        }
        
        public void Initialize(HealthConfig config)
        {
            _config = config;
            MaxValue = _currentHealth.Value = _config.MaxValue;
            _amountHealthPercentage.Value = 1f;
        }
        
        public void ResetHealthStat()
        {
            MaxValue = _currentHealth.Value = _config.MaxValue;
            _amountHealthPercentage.Value = 1f;
        }
        
        public void SetDamage(float value)
        {
            Preconditions.CheckValidateData(value);

            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_amountHealthPercentage.Value - value / MaxValue, 0f, 1f);
            
            _operationWithHealth?.InvokeHit();
            
            if (_currentHealth.Value != 0f) 
                return;
            
            _operationWithHealth?.InvokeDead();
        }

        public async UniTaskVoid AddHealth(float value)
        {
            Preconditions.CheckValidateData(value);

            _currentHealth.Value = Mathf.Clamp(value + _currentHealth.Value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_currentHealth.Value / MaxValue, 0f, 1f);
            
            await UniTask.Yield();
        }

        public async UniTaskVoid SetHealth(float value)
        {
            Preconditions.CheckValidateData(value);

            _currentHealth.Value = Mathf.Clamp(value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_currentHealth.Value / MaxValue, 0f, 1f);
            
            await UniTask.Yield();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _currentHealth?.Dispose();
        }
    }
}