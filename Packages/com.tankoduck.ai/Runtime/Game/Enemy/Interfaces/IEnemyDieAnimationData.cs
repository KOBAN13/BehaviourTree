using DG.Tweening;

namespace Game.Enemy.Interfaces
{
    public interface IEnemyDieAnimationData
    { 
        public float DelayToStartAnimation { get; }
        public float AnimationDuration { get; } 
        public float ScaleShrinkPower { get; } 
        public Ease ScaleEase { get; }
    }
}