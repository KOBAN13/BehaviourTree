using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Stats.Impl;
using Game.Stats.Interface;
using R3;
using Stats.Interface;
using UnityEngine;

namespace Game.Stats
{
    public class RestoringHealthCharacter : IHealthStats, ICharacterStatConfig<HealthConfig>, IHealthRestoring, IDisposable
    {
        public float MaxValue { get; private set; }
        public float CurrentHealth => _healthStats.CurrentHealth;
        public ReadOnlyReactiveProperty<float> CurrentHealthPercentage => _healthStats.CurrentHealthPercentage;
        public ECharacterStat StatType => ECharacterStat.RestoringHealth;
        public Observable<float> OnCurrentValueChanged => _healthStats.OnCurrentValueChanged;
        public bool IsHealthRestoringAfterHitEnemy { get; set; }
        public bool IsHealthRestoringAfterDieEnemy { get; set; } = false;
        
        private Tween _tween;
        private float _previousHealth;
        
        private readonly IHealthStats _healthStats;
        private IHealthConfig _healthConfig;
        private readonly CompositeDisposable _compositeDisposable = new();
        private CancellationTokenSource _cancellationTokenSource;

        public RestoringHealthCharacter(IHealthStats healthStats)
        {
            _healthStats = healthStats ?? throw new ArgumentNullException(nameof(healthStats));
        }

        public void SetDamage(float value)
        {
            CancelRestoration();
            _healthStats.SetDamage(value);
        }
        
        public void Initialize(HealthConfig config)
        {
            _healthConfig = config ?? throw new ArgumentNullException(nameof(config));
            
            MaxValue = config.MaxValue;
        }

        public void ResetHealthStat()
        {
            //TODO Когда добавим возможность возрождения игрока, нужно будет сделать так, чтобы он восстановил здоровье до максимума
        }

        public async UniTaskVoid AddHealth(float value = 0f)
        {
            if (IsHealthRestoringAfterHitEnemy) 
                return;
            
            try
            {
                IsHealthRestoringAfterHitEnemy = true;
                _cancellationTokenSource = new CancellationTokenSource();
                
                _previousHealth = _healthStats.CurrentHealth;

                if (IsHealthRestoringAfterDieEnemy)
                {
                    var restoreValue = MaxValue * _healthConfig.CoefficientRecoveryHealth;
                    _healthStats.AddHealth(restoreValue);
                    return;
                }

                var restoringHealth = MaxValue * _healthConfig.CoefficientRecoveryHealth;
                await RestoreHealth(restoringHealth, _healthConfig.TimeRecoveryHealth);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Health restoration was canceled");
            }
            finally
            {
                IsHealthRestoringAfterHitEnemy = false;
            }
        }

        public async UniTaskVoid SetHealth(float value)
        {
            _healthStats.SetHealth(value);
            
            await UniTask.Yield();
        }

        private async UniTask RestoreHealth(float targetHealth, float duration)
        {
            var tcs = new UniTaskCompletionSource<bool>();

            _tween = DOTween.To(
                    () => 0f,
                    currentHealth => _healthStats.SetHealth(currentHealth + _previousHealth),
                    targetHealth,
                    duration
                )
                .OnComplete(() => tcs.TrySetResult(true))
                .OnKill(() => tcs.TrySetCanceled());

            await tcs.Task.AttachExternalCancellation(_cancellationTokenSource.Token);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f), cancellationToken: _cancellationTokenSource.Token);
        }

        private void CancelRestoration()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _tween?.Kill();
            _tween = null;
            IsHealthRestoringAfterHitEnemy = false;
        }

        public void Dispose()
        {
            CancelRestoration();
            _compositeDisposable.Dispose();
        }
    }
}