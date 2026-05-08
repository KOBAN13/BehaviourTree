using System.Collections.Generic;
using Game.AI.Enemies;
using Game.Infrastructure.Helpers.Constants;
using GOAP;

namespace Game.AI.Blackboard
{
    public class BlackboardConstructor : IBlackboardConstructor, IEnemyVisitor
    {
        private readonly BlackboardController _blackboardController = new();

        public void SetEnemySensors(IReadOnlyDictionary<ESensorType, ISensor> sensors)
        {
            foreach (var keyValueSensor in sensors)
            {
                _blackboardController.SetValue(keyValueSensor.Key.ToString(), keyValueSensor.Value);
            }
        }
        
        public void SetBlackboardData(AEnemy enemy)
        {
            enemy.Accept(this);
        }
        
        public BlackboardController GetBlackboardController() => _blackboardController;
        
        public void Visit(MeleeEnemyData data)
        {
            data.BlackboardData.Apply(_blackboardController);
            
            _blackboardController.SetValue(BlackboardDataKeys.EnemyInstance, data.Enemy);
            _blackboardController.SetValue(BlackboardDataKeys.AgentData, data.AgentData);
            _blackboardController.SetValue(BlackboardDataKeys.AgentRenderer, data.Renderers);
            _blackboardController.SetValue(BlackboardDataKeys.AnimationBrain, data.AnimationBrain);
            _blackboardController.SetValue(BlackboardDataKeys.NavMeshAgent, data.Agent);
            _blackboardController.SetValue(BlackboardDataKeys.PlayerTarget, data.TargetAim);
            _blackboardController.SetValue(BlackboardDataKeys.AgentTransform, data.AgentTransform);
            _blackboardController.SetValue(BlackboardDataKeys.AgentModelTransform, data.AgentModelTransform);
            _blackboardController.SetValue(BlackboardDataKeys.AgentMeleeWeapon, data.MeleeWeapon);
        }

        public void Visit(ProjectileEnemyData data)
        {
            
        }
    }
}