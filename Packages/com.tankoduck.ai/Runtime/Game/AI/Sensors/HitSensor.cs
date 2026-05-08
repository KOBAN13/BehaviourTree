using System;
using Game.Infrastructure;
using Game.Infrastructure.Extension;
using Game.Infrastructure.Helpers;
using R3;
using UnityEngine;

namespace GOAP
{
    [Serializable]
    [RequireComponent(typeof(SphereCollider))]
    public class HitSensor : MonoBehaviour, ISensor
    {
        [SerializeField] private float _timeAggression;
        [SerializeField] private float _radiusDetect;
        [SerializeField] private SphereCollider _trigger;
        
        public ReadOnlyReactiveProperty<bool> IsActiveSensor => _isActiveSensor;
        public bool IsActivate => _isActiveSensor.Value;
        
        private readonly ReactiveProperty<bool> _isActiveSensor = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private void Awake()
        {
            _trigger.isTrigger = true;
            _trigger.radius = _radiusDetect;
        }

        private void UnsubscribeUpdate()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        private void OnDestroy()
        {
            UnsubscribeUpdate();
            _isActiveSensor.Dispose();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.IsOnLayer(LayerMasks.Player)) return;
            
            _isActiveSensor.Value = true;
        }

        
        private void OnTriggerExit(Collider other)
        {
            if (!other.IsOnLayer(LayerMasks.Player)) return;
            
            _isActiveSensor.Value = false; 
        }
    }
}