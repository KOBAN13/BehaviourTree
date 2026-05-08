using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.AI.Weapon.Config
{
    [CreateAssetMenu(fileName = nameof(MeleeWeaponData), menuName = "Enemy/Weapon" + nameof(MeleeWeaponData))]
    public class MeleeWeaponData : AEnemyWeaponData
    {
        [field: Title("Weapon Parameters")]
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public ParticleSystem HitEffect { get; protected set; }
        [field: Title("Physics Parameters")]
        [field: SerializeField] public float RadiusSphere { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
    }
}