using Game.AI.BehaviourTree.Nodes;
using Game.AI.Blackboard;
using Game.AI.Weapon;
using GOAP;
using R3;
using UnityEngine;

namespace Game.AI.Enemies
{
    [RequireComponent(typeof(ReceiveDamageSensor))]
    [RequireComponent(typeof(AttackSensor))]
    public class MeleeEnemy : AEnemy
    {
        private MeleeEnemyData _agentData;
        private BehaviourTreeNode _behaviourTreeNode;

        public override void FirstInitialize()
        { 
            //todo
            //1. Огромная дрочь с названиями строками 4 класса с хуевой горой строк
            //2. Данные заполняются в balckboard с 3 мест, не очевидно и не удобно
            //3. Нужно для blackboard реалзиовать генератор который все данные заполнит
            
            Observable.EveryUpdate()
                .Where(_ => _behaviourTreeNode != null && isActiveAndEnabled && IsAgentReady(AgentData.NavMeshAgent))
                .Subscribe(_ => _behaviourTreeNode.Process())
                .AddTo(CompositeDisposable);
            
            _agentData = new MeleeEnemyData(
                AgentData.AnimationBrain,
                AgentData.NavMeshAgent,
                PlayerTarget.GetPosition(), 
                transform, 
                this,
                AgentData, 
                AgentData.Config,
                AgentData.ModelTransform,
                AgentData.Renderers,
                AgentData.EnemyWeapon
            );
            
            _behaviourTreeNode = BehaviourTreeAgent.OnFirstInitialize(this);
            
            
            IsInitialized = true;
        }

        public override async void OnActivate(Vector3 spawnPosition)
        {
            await ReturnAgent(spawnPosition, AgentData.NavMeshAgent);
            
            Observable.EveryUpdate()
                .Where(_ => _behaviourTreeNode != null && isActiveAndEnabled && IsAgentReady(AgentData.NavMeshAgent))
                .Subscribe(_ => _behaviourTreeNode.Process())
                .AddTo(CompositeDisposable);
            
            BehaviourTreeAgent.OnActivate(this);
        }

        public override void OnDeactivate()
        {
            _behaviourTreeNode.Reset();
            BehaviourTreeAgent.OnDeactivate(this);
            
            ResetAgent(AgentData.NavMeshAgent);
            ResetDissolveShader();
            
            EnemySpawner.ReturnEnemyAfterDie(this, AgentData.EnemyType);
        }

        public override void Accept(IEnemyVisitor enemyVisitor)
        {
            enemyVisitor.Visit(_agentData);
        }
    }
}