using System;
using GOAP;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI.Blackboard
{
    [Serializable]
    public struct Value
    {
        public EValueType Type;
        
        public bool BoolValue;
        public int IntValue;
        public float FloatValue;
        
        public static implicit operator int(Value value) => value.ConvertTo<int>();
        public static implicit operator float(Value value) => value.ConvertTo<float>();
        public static implicit operator bool(Value value) => value.ConvertTo<bool>();
        
        public static implicit operator NavMeshAgent (Value value) => value.ConvertTo<NavMeshAgent>();
        public static implicit operator Transform (Value value) => value.ConvertTo<Transform>();


        private T ConvertTo<T>()
        {
            switch (Type)
            {
                case EValueType.Bool:
                    return AsBool<T>(BoolValue);
                case EValueType.Int:
                    return AsInt<T>(IntValue);
                case EValueType.Float:
                    return AsFloat<T>(FloatValue);
                default:
                    throw new NotSupportedException($"Not supported value type: {typeof(T)}");
            }
        }
        
        private static T AsBool<T>(bool value) => typeof(T) == typeof(bool) && value is T convertedValue ? convertedValue : default;
        private static T AsInt<T>(int value) => typeof(T) == typeof(int) && value is T convertedValue ? convertedValue : default;
        private static T AsFloat<T>(float value) => typeof(T) == typeof(float) && value is T convertedValue ? convertedValue : default;
        private static T AsNavMeshAgent<T>(NavMeshAgent value) => typeof(T) == typeof(NavMeshAgent) && value is T convertedValue ? convertedValue : default;
        private static T AsTransform<T>(Transform value) => typeof(T) == typeof(Transform) && value is T convertedValue ? convertedValue : default;
    }
}