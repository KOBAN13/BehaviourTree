using Animation;
using Animation.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Blackboard
{
    public readonly struct ProjectileEnemyData
    {
        public readonly AnimationBrain<EEnemyAnimationType> AnimationBrain;
        public readonly NavMeshAgent Agent;
        public readonly Transform TargetAim;
        public readonly Transform AgentTransform;

        public ProjectileEnemyData(
            AnimationBrain<EEnemyAnimationType> animationBrain, 
            NavMeshAgent agent, 
            Transform targetAim, 
            Transform agentTransform
        )
        {
            AnimationBrain = animationBrain;
            Agent = agent;
            TargetAim = targetAim;
            AgentTransform = agentTransform;
        }
    }
}