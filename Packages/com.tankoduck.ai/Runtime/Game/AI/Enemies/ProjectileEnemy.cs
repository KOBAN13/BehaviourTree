using Game.AI.Blackboard;
using GOAP;
using UnityEngine;

namespace Game.AI.Enemies
{
    [RequireComponent(typeof(EyesSensor))]
    public class ProjectileEnemy : AEnemy
    {
        private ProjectileEnemyData _agentData;
        
        public override void FirstInitialize()
        {
            _agentData = new ProjectileEnemyData();
        }

        public override void OnActivate(Vector3 position)
        {
            
        }

        public override void OnDeactivate()
        {
            
        }


        public override void Accept(IEnemyVisitor enemyVisitor)
        {
            enemyVisitor.Visit(_agentData);
        }
    }
}