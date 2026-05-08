using System.Collections.Generic;
using Animation.Enums;

namespace Animation.Interface
{
    public interface IEnemyAnimancerParameters
    {
        IReadOnlyDictionary<EEnemyAnimationType, MotionClipTransition> CharacterAnimation { get; }
    }
}