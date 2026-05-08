using Cysharp.Threading.Tasks;
using Game.AI.Agent;
using Game.AI.Blackboard;
using Game.Enemy.Interfaces;
using Game.Infrastructure.Helpers;
using Game.Player;
using Game.Stats;
using Game.Stats.Interface;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using VContainer;

namespace Game.AI.Enemies
{
    public abstract class AEnemy : SerializedMonoBehaviour, IDamageable
    {
        [field: SerializeField] public AAgentData AgentData { get; private set; }
        public IPlayerMovement PlayerTarget { get; private set; }
        public bool IsInitialized { get; protected set; }
        
        protected CharacterStats CharacterStats;
        protected CompositeDisposable CompositeDisposable = new();
        protected IBehaviourTreeAgent BehaviourTreeAgent = new BehaviourTreeAgent();
        protected IEnemySpawner EnemySpawner;

        //Debug
        [SerializeField] private Image _healthBarImage;

        private MaterialPropertyBlock _propertyBlock;
        private IHealthStats _healthStats;
        
        public abstract void FirstInitialize();
        public abstract void OnActivate(Vector3 spawnPosition);
        public abstract void OnDeactivate();
        public abstract void Accept(IEnemyVisitor enemyVisitor);

        [Inject]
        private void Construct(IEnemySpawner enemySpawner, IPlayerMovement playerTarget)
        {
            PlayerTarget = playerTarget;
            EnemySpawner = enemySpawner;
        }

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            CharacterStats = new CharacterStats();
                        
            CharacterStats.AddStat(new HealthCharacter(), AgentData.HealthConfig);
            
            _healthStats = CharacterStats.GetStat<IHealthStats>(ECharacterStat.Health);
            
            CompositeDisposable = new CompositeDisposable();

            _healthStats.CurrentHealthPercentage.Subscribe(percentage =>
                {
                    _healthBarImage.fillAmount = percentage;
                })
                .AddTo(CompositeDisposable);
        }

        private void OnEnable()
        {
            AgentData.Collider.enabled = true;
            AgentData.ModelTransform.localScale = Vector3.one;
            _healthStats.ResetHealthStat();
        }

        public float GetHealth() => _healthStats.CurrentHealth;

        public void SetDamage(float value)
        {
            _healthStats.SetDamage(value);
        }

        private void OnDestroy()
        {
            CompositeDisposable.Clear();
            CompositeDisposable.Dispose();
        }
        
        protected bool IsAgentReady(NavMeshAgent agent)
        {
            return agent != null 
                   && agent.isActiveAndEnabled 
                   && agent.isOnNavMesh 
                   && !agent.isStopped;
        }

        protected void ResetAgent(NavMeshAgent agent)
        {
            agent.isStopped = true; 
            agent.ResetPath();
            agent.enabled = false; 
        }
        
        protected async UniTask ReturnAgent(Vector3 spawnPosition, NavMeshAgent agent)
        {
            agent.enabled = true;
            
            if (!agent.isOnNavMesh) 
            {
                agent.Warp(spawnPosition);
            }

            await InitializeAfterFrame();
        }

        protected void ResetDissolveShader()
        {
            foreach (var meshRenderers in AgentData.Renderers)
            {
                meshRenderers.GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(ShaderId.DissolveHash, 0f);
                meshRenderers.SetPropertyBlock(_propertyBlock);
            }
        }

        private async UniTask InitializeAfterFrame()
        {
            await UniTask.Yield();

            if (AgentData.NavMeshAgent.isOnNavMesh && AgentData.NavMeshAgent.isActiveAndEnabled)
            {
                AgentData.NavMeshAgent.isStopped = false;
            }
        }

        //TODO вынести в отдельный сервис
        // private async void SpawnExpAfterDie() =>
        //     await _expDropSpawner.SpawnExpAsync();
    }
}