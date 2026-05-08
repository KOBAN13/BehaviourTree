using System;
using Animation.Enums;

namespace Animation.Interface
{
    public interface IAnimationBrain<T> where T : Enum
    {
        void PlayAnimation(T type, float speed = 1f, float fadeTime = 0.2f);
        void PlayAnimationWithoutCancel(T type, float speed = 1f, float fadeTime = 0.2f);
        void SetDefaultAnimation(T type, float speed = 1f, float fadeTime = 0.2f);
        void PlayForce(AnimationRequest<T> request);
        float GetAnimationLength(T type);
    }
}