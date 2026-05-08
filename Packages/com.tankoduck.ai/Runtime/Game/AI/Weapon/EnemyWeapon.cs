using Game.AI.Weapon.Config;
using Game.Enemy.Interfaces;
using UnityEngine;

namespace Game.AI.Weapon
{
    public abstract class AEnemyWeapon : MonoBehaviour
    {
        [field: SerializeField] public AEnemyWeaponData Data { get; private set; }
        
        protected float LastAttackTime;
        protected bool CanAttack => Time.time >= LastAttackTime + Data.AttackCooldown;
        
        public abstract void InitializeWeapon();
        public abstract bool TryAttack(Transform target);
        
        public virtual void DealDamage(IDamageable damageable)
        {
            damageable.SetDamage(Data.Damage);
        }
    }
}