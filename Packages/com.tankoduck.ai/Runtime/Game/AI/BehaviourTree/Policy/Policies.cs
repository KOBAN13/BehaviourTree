namespace Game.AI.BehaviourTree.Policy
{
    public static class Policies
    {
        public static readonly IPolicy RunForeverPolicy = new RunForeverPolicy();
        public static readonly IPolicy RunUntilSuccessPolicy = new RunUntilSuccessPolicy();
        public static readonly IPolicy RunUntilFailPolicy = new RunUntilFailPolicy();
    }
}