using Animation;
using Animation.Enums;
using BlackboardScripts;
using Game.AI.BehaviourTree.Nodes.Action;
using Game.AI.Blackboard;
using Game.Infrastructure.Helpers.Constants;
using UnityEngine.AI;

namespace Game.AI.Strategy
{
    public class IdleStrategy : IActionStrategy
    {
        public bool CanPerform => true;
        public bool Complete => !_agent.pathPending && !_agent.hasPath;
        
        private readonly NavMeshAgent _agent;
        private readonly AnimationBrain<EEnemyAnimationType> _animationBrain;

        public IdleStrategy(BlackboardController blackboardController)
        {
            _agent = blackboardController.GetValue<NavMeshAgent>(BlackboardDataKeys.NavMeshAgent);
            _animationBrain = blackboardController.GetValue<AnimationBrain<EEnemyAnimationType>>(BlackboardDataKeys.AnimationBrain);
        }

        public void Start()
        {
            _animationBrain.SetDefaultAnimation(EEnemyAnimationType.Idle);
        }
    }
}