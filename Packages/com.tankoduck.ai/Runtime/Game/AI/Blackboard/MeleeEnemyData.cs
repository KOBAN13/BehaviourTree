using Animation;
using Animation.Enums;
using Game.AI.Agent;
using Game.AI.Enemies;
using Game.AI.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Blackboard
{
    public readonly struct MeleeEnemyData
    {
        public readonly IBlackboardData BlackboardData;
        public readonly AnimationBrain<EEnemyAnimationType> AnimationBrain;
        public readonly NavMeshAgent Agent;
        public readonly Transform TargetAim;
        public readonly Transform AgentTransform;
        public readonly Transform AgentModelTransform;
        public readonly AEnemy Enemy;
        public readonly AAgentData AgentData;
        public readonly Renderer[] Renderers;
        public readonly AEnemyWeapon MeleeWeapon;

        public MeleeEnemyData(
            AnimationBrain<EEnemyAnimationType> animationBrain, 
            NavMeshAgent agent, 
            Transform targetAim, 
            Transform agentTransform, 
            AEnemy enemy, 
            AAgentData agentData, 
            IBlackboardData blackboardData, 
            Transform agentModelTransform, 
            Renderer[] renderers, 
            AEnemyWeapon meleeWeapon
        )
        {
            AnimationBrain = animationBrain;
            Agent = agent;
            TargetAim = targetAim;
            AgentTransform = agentTransform;
            Enemy = enemy;
            AgentData = agentData;
            BlackboardData = blackboardData;
            AgentModelTransform = agentModelTransform;
            Renderers = renderers;
            MeleeWeapon = meleeWeapon;
        }
    }
}