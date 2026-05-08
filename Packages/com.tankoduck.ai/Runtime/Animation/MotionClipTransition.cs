using System;
using Animancer;
using Animation.Enums;
using UnityEngine;

namespace Animation
{
    [Serializable]
    [EventNames(typeof(EAnimancerEvents))]
    public class MotionClipTransition : ClipTransition
    {
        [SerializeField] private bool applyRootMotion;
        
        public EAnimationLayer AnimationLayer;
        
        public override void Apply(AnimancerState state)
        {
            base.Apply(state);

            var animator = state.Graph?.Component?.Animator;
            if (animator != null)
                animator.applyRootMotion = applyRootMotion;

            state.LayerIndex = (int)AnimationLayer;
        }
    }
}
