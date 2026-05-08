using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.AI.Weapon.Config;
using Game.Enemy.Interfaces;
using Game.Infrastructure;
using Game.Infrastructure.Helpers;
using UnityEngine;

namespace Game.AI.Weapon
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : AEnemyWeapon
    {
        [SerializeField] private Collider _hitbox;

        private MeleeWeaponData _data;
        private Transform _target;

        public override void InitializeWeapon()
        {
            _data = Data as MeleeWeaponData;
        }

        public override bool TryAttack(Transform target)
        {
            _target = target;

            if (!CanAttack)
                return false;

            var distance = Vector3.Distance(transform.position, target.position);

            if (distance > _data.AttackRange) return false;

            ActivateHitBox().Forget();

            var direction = (target.position - transform.position).normalized;

            if (!UnityEngine.Physics.SphereCast(transform.position, _data.RadiusSphere, direction, out var hitInfo,
                    _data.MaxDistance, LayerMasks.Player)) return false;

            if (TryAttack(out var damageable, target))
            {
                var particle = Instantiate(_data.HitEffect, hitInfo.point, Quaternion.identity);
                particle.Play();
                DealDamage(damageable);
            }

            LastAttackTime = Time.time;

            return true;
        }

        private bool TryAttack(out IDamageable damageable, Component target)
        {
            return target.TryGetComponent(out damageable);
        }

        private async UniTaskVoid ActivateHitBox()
        {
            _hitbox.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f)); //посчитать время анимации
            _hitbox.enabled = false;
        }

        /*private void OnDrawGizmos()
        {
            if (_target == null || _data == null)
                return;

            // Направление атаки
            Vector3 direction = (_target.position - transform.position).normalized;

            // Рисуем сферу в начальной точке
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _data.AttackRange);

            // Рисуем линию направления атаки
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + direction * _data.AttackRange);

            // Если SphereCast не попал, рисуем красную сферу в конце
            if (!UnityEngine.Physics.SphereCast(transform.position, 2f, direction, out var hitInfo,
                    _data.AttackRange, _data.TargetLayer))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + direction * _data.AttackRange, _data.AttackRange);
            }
            else // Если попал, рисуем зелёную сферу в точке попадания
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(hitInfo.point, _data.AttackRange);
            }
        }*/
    }
}