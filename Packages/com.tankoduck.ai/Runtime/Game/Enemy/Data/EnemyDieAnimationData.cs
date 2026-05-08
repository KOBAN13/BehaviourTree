using DG.Tweening;
using Game.Enemy.Interfaces;
using UnityEngine;

namespace Game.Enemy.Data
{
    [CreateAssetMenu(fileName = nameof(EnemyDieAnimationData), menuName = "Enemy/" + nameof(EnemyDieAnimationData))]
    public class EnemyDieAnimationData : ScriptableObject, IEnemyDieAnimationData
    {
        [field: SerializeField] public float DelayToStartAnimation { get; private set; }
        [field: SerializeField] public float AnimationDuration { get; private set; }
        [field: SerializeField] public float ScaleShrinkPower { get; private set; }
        [field: SerializeField] public Ease ScaleEase { get; private set; } = Ease.InBack; 
    }
}