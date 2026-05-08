using System.Collections.Generic;
using Game.AI.Enemies;
using GOAP;

namespace Game.AI.Blackboard
{
    public interface IBlackboardConstructor
    {
        void SetEnemySensors(IReadOnlyDictionary<ESensorType, ISensor> sensors);
        void SetBlackboardData(AEnemy data);
        BlackboardController GetBlackboardController();
    }
}