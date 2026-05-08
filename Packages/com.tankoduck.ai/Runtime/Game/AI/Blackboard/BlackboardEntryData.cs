using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Blackboard
{
    [Serializable]
    public class BlackboardEntryData : ISerializationCallbackReceiver
    {
        public string key;
        public EValueType type;
        public Value value;

        private static Dictionary<EValueType, Action<Blackboard, BlackboardKey, Value>> _actions = new()
        {
            { EValueType.Bool, (blackboard, key, value) => blackboard.SetValue<bool>(key, value) },
            { EValueType.Int, (blackboard, key, value) => blackboard.SetValue<int>(key, value) },
            { EValueType.Float, (blackboard, key, value) => blackboard.SetValue<float>(key, value) }
        };

        public void SetValueOnBlackboard(BlackboardController blackboard)
        {
            _actions[type](blackboard.GetBlackboard(), blackboard.GetBlackboard().GetOrRegisterKey(key), value);
        }


        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize() => value.Type = type;
    }
}