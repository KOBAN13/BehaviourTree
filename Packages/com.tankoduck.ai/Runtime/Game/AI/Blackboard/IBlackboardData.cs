using System.Collections.Generic;

namespace Game.AI.Blackboard
{
    public interface IBlackboardData
    {
        IReadOnlyList<BlackboardEntryData> Entries { get; }
        
        void Apply(BlackboardController blackboard);
    }
}