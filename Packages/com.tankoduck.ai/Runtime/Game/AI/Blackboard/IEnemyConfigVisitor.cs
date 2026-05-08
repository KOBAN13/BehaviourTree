namespace Game.AI.Blackboard
{
    public interface IEnemyVisitor
    {
        void Visit(MeleeEnemyData config);
        void Visit(ProjectileEnemyData config);
    }
}