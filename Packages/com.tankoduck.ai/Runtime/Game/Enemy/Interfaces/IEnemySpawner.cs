using Game.AI.Enemies;

namespace Game.Enemy.Interfaces
{
    public interface IEnemySpawner
    {
        void ReturnEnemyAfterDie(AEnemy enemy, EEnemyType enemyType);
    }
}