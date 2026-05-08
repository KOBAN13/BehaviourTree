using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Blackboard
{
    [CreateAssetMenu(fileName = nameof(BlackboardData), menuName = "Enemy/" + nameof(BlackboardData))]
    public class BlackboardData : ScriptableObject, IBlackboardData
    {
        [SerializeField] private List<BlackboardEntryData> _entries;
        
        public IReadOnlyList<BlackboardEntryData> Entries => _entries;
        
        public void Apply(BlackboardController blackboard)
        {
            foreach (var entry in _entries)
            {
                entry.SetValueOnBlackboard(blackboard);
            }
        }
    }
}