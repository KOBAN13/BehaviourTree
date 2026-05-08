using System;
using System.Collections.Generic;
using Animation.Enums;

namespace Animation.Interface
{
    public interface IPlayerAnimanсerParameters<T> where T : Enum
    {
        IReadOnlyDictionary<T, MotionClipTransition> CharacterAnimation { get; }
    }
}