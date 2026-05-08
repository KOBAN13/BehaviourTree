using UnityEngine;

namespace Game.AI.Weapon.Config
{
    public abstract class AEnemyWeaponData : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; protected set; }
        [field: SerializeField] public float AttackCooldown { get; protected set; }
        [field: SerializeField] public LayerMask TargetLayer { get; protected set; }
    }
}