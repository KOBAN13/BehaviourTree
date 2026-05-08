using System;
using System.Threading;
using Animancer;
using Animation.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Animation
{
    public class AnimationBrain<T> : IAnimationBrain<T> where T : Enum
    {
        private readonly AnimancerComponent _animancerComponent;
        private readonly IPlayerAnimanсerParameters<T> _playerAnimancerParameters;
        
        private CancellationTokenSource _cancellationToken;
        private T _currentForceAnimationType;
        private T _defaultAnimationType;

        public AnimationBrain(
            IPlayerAnimanсerParameters<T> playerAnimancerParameters, 
            AnimancerComponent animancerComponent
        )
        {
            _playerAnimancerParameters = playerAnimancerParameters;
            _animancerComponent = animancerComponent;
        }

        public void PlayAnimation(T type, float speed = 1f, float fadeTime = 0.2f)
        {
            var clip = GetClip(type);
            
            _cancellationToken?.Cancel();
            
            var state = _animancerComponent.Play(clip, fadeTime);
            
            if(state.Speed != 0f)
                state.Speed = speed;
        }
        
        public void PlayAnimationWithoutCancel(T type, float speed = 1f, float fadeTime = 0.2f)
        {
            var clip = GetClip(type);
            
            _cancellationToken?.Cancel();
            
            if(_animancerComponent.States.Current != null && _animancerComponent.States.Current.Clip == clip.Clip)
                return;
            
            var state = _animancerComponent.Play(clip, fadeTime);
            
            if(state.Speed != 0f)
                state.Speed = speed;
        }

        public void SetDefaultAnimation(T type, float speed = 1f, float fadeTime = 0.2f)
        {
            _defaultAnimationType = type;
            
            _cancellationToken?.Cancel();
            _currentForceAnimationType = (T)Enum.GetValues(typeof(T)).GetValue(0);

            var state = _animancerComponent.Play(GetClip(type), fadeTime);
            
            if(state.Speed != 0f)
                state.Speed = speed;
        }

        public void PlayForce(AnimationRequest<T> request)
        {
            _cancellationToken?.Cancel();
            _currentForceAnimationType = request.Type;

            _cancellationToken = new CancellationTokenSource();
            
            PlayForceAnimation(request).Forget();
        }

        public float GetAnimationLength(T type)
        {
            var clip = GetClip(type);
            
            return clip.Clip.length;
        }

        private MotionClipTransition GetClip(T type)
        {
            if(!_playerAnimancerParameters.CharacterAnimation.TryGetValue(type, out var animationClip))
                Debug.LogError($"Dont find animation clip by type: {type}");

            return animationClip;
        }

        private async UniTaskVoid PlayForceAnimation(AnimationRequest<T> request)
        {
            var clip = GetClip(request.Type);

            var state = _animancerComponent.Play(clip, request.FadeDuration);
            
            if(request.Speed != 0f)
                state.Speed = request.Speed;
            
            _animancerComponent.Animator.applyRootMotion = request.ApplyRootMotion;
            
            state.LayerIndex = (int)request.AnimationLayer;
            
            try 
            {
                await UniTask.Delay(TimeSpan.FromSeconds(clip.Clip.length * request.Delta), cancellationToken: _cancellationToken.Token);
                
                _ = _animancerComponent.Play(GetClip(_defaultAnimationType), clip.Clip.length * (1 - request.Delta));
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                _currentForceAnimationType = (T)Enum.GetValues(typeof(T)).GetValue(0);
                _cancellationToken = null;
            }
        }
    }
}
