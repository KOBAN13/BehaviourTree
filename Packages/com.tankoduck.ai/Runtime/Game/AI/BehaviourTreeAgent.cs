using Game.AI.BehaviourTree;
using Game.AI.BehaviourTree.Debugger;
using Game.AI.BehaviourTree.Nodes;
using Game.AI.Blackboard;
using Game.AI.Enemies;
using Game.Enemy;

namespace Game.AI
{
    public class BehaviourTreeAgent : IBehaviourTreeAgent
    {
        private readonly IBlackboardConstructor _blackboardConstructorMeleeEnemy = new BlackboardConstructor();
        private readonly IBlackboardConstructor _blackboardConstructorProjectileEnemies = new BlackboardConstructor();
        private readonly BehaviourTreeConstructor _behaviourTreeConstructor;
        private readonly IBTDebugger _debugger = new BTDebugger();

        public BehaviourTreeAgent()
        {
            _behaviourTreeConstructor = new BehaviourTreeConstructor(_debugger);
        }
        
        public BehaviourTreeNode OnFirstInitialize(AEnemy enemy)
        {
            BehaviourTreeNode behaviourTreeNode = null;
            
            switch (enemy.AgentData.EnemyType)
            {
                case EEnemyType.CloseСombat:
                    _blackboardConstructorMeleeEnemy.SetEnemySensors(enemy.AgentData.Sensors);
                    _blackboardConstructorMeleeEnemy.SetBlackboardData(enemy);
                    behaviourTreeNode = _behaviourTreeConstructor.CreateMeleeEnemyBehaviourTree(
                        _blackboardConstructorMeleeEnemy, 
                        enemy as MeleeEnemy
                    );
                    break;
                case EEnemyType.ProjectileEnemies:
                    behaviourTreeNode = null;
                    break;
            }
            
            return behaviourTreeNode;
        }

        public void OnActivate(AEnemy enemy)
        {
            
        }

        public void OnDeactivate(AEnemy enemy)
        {
            
        }
    }
}