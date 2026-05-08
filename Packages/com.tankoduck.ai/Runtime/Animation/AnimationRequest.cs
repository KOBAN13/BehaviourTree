using System;
using Animation.Enums;

namespace Animation
{
    public struct AnimationRequest<T> where T : Enum
    {
        public readonly T Type;
        public readonly bool ApplyRootMotion;
        public readonly EAnimationLayer AnimationLayer;
        public readonly float FadeDuration;
        public readonly float Delta;
        public readonly float Speed;

        public AnimationRequest(T type, bool applyRootMotion, EAnimationLayer animationLayer, float delta, float fadeDuration = 0.2f, float speed = 1f)
        {
            Type = type;
            ApplyRootMotion = applyRootMotion;
            AnimationLayer = animationLayer;
            Delta = delta;
            FadeDuration = fadeDuration;
            Speed = speed;
        }
    }
}