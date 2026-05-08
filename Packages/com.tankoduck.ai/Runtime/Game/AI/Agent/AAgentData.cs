using System.Collections.Generic;
using Animancer;
using Animation;
using Animation.Data;
using Animation.Enums;
using Game.AI.Blackboard;
using Game.AI.Weapon;
using Game.Enemy;
using Game.Enemy.Data;
using Game.Stats.Impl;
using GOAP;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Stats.Impl;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AnimancerComponent))]
    public abstract class AAgentData : SerializedMonoBehaviour
    {
        [field: SerializeField] public EEnemyType EnemyType { get; protected set; }
        [field: SerializeField] public HealthConfig HealthConfig { get; protected set; }
        [field: SerializeField] public StaminaConfig StaminaConfig { get; protected set; }
        [field: SerializeField] public EnemyDieAnimationData EnemyDieAnimationData { get; protected set; }
        [field: SerializeField] public EnemyAnimancerParameters CharacterAnimancerParameters { get; protected set; }
        [field: SerializeField] public AEnemyWeapon EnemyWeapon { get; private set; }
        [field: SerializeField] public Collider Collider { get; protected set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public AnimancerComponent AnimancerComponent { get; private set; }
        [field: SerializeField] public Transform ModelTransform { get; private set; }
        [field: SerializeField] public Renderer[] Renderers { get; private set; }
        
        [field: TitleGroup("Blackboard Data")]
        [field: InlineEditor]
        [field: SerializeField] public BlackboardData Config { get; private set; }

        [field: OdinSerialize] private Dictionary<ESensorType, ISensor> _sensorsData;
        
        public IReadOnlyDictionary<ESensorType, ISensor> Sensors => _sensorsData;
        
        public AnimationBrain<EEnemyAnimationType> AnimationBrain { get; private set; }
        
        private void Awake()
        {
            AnimationBrain =
                new AnimationBrain<EEnemyAnimationType>(CharacterAnimancerParameters, AnimancerComponent);
        }
    }
}