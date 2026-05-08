using System;

namespace Game.AI.States
{
    public class EnemyStateMachineData : IDisposable
    {
        public bool IsEnemyDie() => true;
        public bool IsEnemyLife() => true;
        public bool IsEnemyRespawn() => true;
        
        public void Dispose()
        {
            
        }
    }
}