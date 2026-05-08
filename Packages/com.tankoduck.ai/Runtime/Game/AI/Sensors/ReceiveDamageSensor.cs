using System;
using R3;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace GOAP
{
    [Serializable]
    public class ReceiveDamageSensor : MonoBehaviour, ISensor
    {
        public ReadOnlyReactiveProperty<bool> IsActiveSensor => _isActiveSensor;
        public bool IsActivate => _isActiveSensor.Value;
        public Vector3 ImpactDirection { get; private set; }
        
        private readonly ReactiveProperty<bool> _isActiveSensor = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public void OnActivateSensor(Vector3 impactDirection)
        {
            ImpactDirection = impactDirection;
            _isActiveSensor.Value = true;
        }
        
        public void OnDeactivateSensor()
        {
            _isActiveSensor.Value = false;
        }
    }
}