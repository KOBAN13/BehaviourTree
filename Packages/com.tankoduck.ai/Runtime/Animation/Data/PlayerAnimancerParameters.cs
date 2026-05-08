using System.Collections.Generic;
using Animation.Enums;
using Animation.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Animation.Data
{
    [CreateAssetMenu(fileName = nameof(PlayerAnimancerParameters), menuName = "Game/Animation/" + nameof(PlayerAnimancerParameters))]
    public class PlayerAnimancerParameters : SerializedScriptableObject, IPlayerAnimanсerParameters<EPlayerAnimationType>
    {
        [SerializeField] private Dictionary<EPlayerAnimationType, MotionClipTransition> _characterAnimation;
        
        public IReadOnlyDictionary<EPlayerAnimationType, MotionClipTransition> CharacterAnimation => _characterAnimation;
    }
}