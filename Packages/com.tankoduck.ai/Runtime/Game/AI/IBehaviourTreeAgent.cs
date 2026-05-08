using Game.AI.BehaviourTree.Nodes;
using Game.AI.Enemies;

namespace Game.AI
{
    public interface IBehaviourTreeAgent
    {
        BehaviourTreeNode OnFirstInitialize(AEnemy enemy);
        void OnActivate(AEnemy enemy);
        void OnDeactivate(AEnemy enemy);
    }
}