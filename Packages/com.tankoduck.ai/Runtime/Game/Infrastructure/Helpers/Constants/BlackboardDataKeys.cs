namespace Game.Infrastructure.Helpers.Constants
{
    public static class BlackboardDataKeys
    {
        public static string AnimationBrain => nameof(AnimationBrain);
        public static string AgentData => nameof(AgentData);
        public static string NavMeshAgent => nameof(NavMeshAgent);
        public static string AgentTransform => nameof(AgentTransform);
        public static string AgentModelTransform => nameof(AgentModelTransform);
        public static string AgentRenderer => nameof(AgentRenderer);
        public static string AgentMeleeWeapon => nameof(AgentMeleeWeapon);
        public static string PlayerTarget => nameof(PlayerTarget);
        public static string WalkSpeed => nameof(WalkSpeed);
        public static string KnockbackDistance => nameof(KnockbackDistance);
        public static string KnockbackTime => nameof(KnockbackTime);
        public static string RecoveryTime => nameof(RecoveryTime);
        public static string EnemyInstance => nameof(EnemyInstance);
    }
}