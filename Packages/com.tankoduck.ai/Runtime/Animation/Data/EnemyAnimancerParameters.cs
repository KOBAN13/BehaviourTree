using System.Collections.Generic;
using Animation.Enums;
using Animation.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Animation.Data
{
    [CreateAssetMenu(fileName = nameof(EnemyAnimancerParameters), menuName = "Game/Animation/" + nameof(EnemyAnimancerParameters))]
    public class EnemyAnimancerParameters : SerializedScriptableObject, IPlayerAnimanсerParameters<EEnemyAnimationType>
    {
        [SerializeField] private Dictionary<EEnemyAnimationType, MotionClipTransition> _characterAnimation;
        
        public IReadOnlyDictionary<EEnemyAnimationType, MotionClipTransition> CharacterAnimation => _characterAnimation;
    }
}