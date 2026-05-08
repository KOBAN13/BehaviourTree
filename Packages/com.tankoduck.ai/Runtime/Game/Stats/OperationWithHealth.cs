using System;
using R3;

namespace Game.Stats
{
    public class OperationWithHealth : IDisposable
    {
        private readonly Subject<Unit> _hit = new();
        private readonly Subject<Unit> _die = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public void SubscribeHit(Action hit = null)
        { 
            _hit?.Subscribe(_ => hit?.Invoke()).AddTo(_compositeDisposable); 
        }

        public void SubscribeDie(Action dead = null)
        {
            _die?.Subscribe(_ => dead?.Invoke()).AddTo(_compositeDisposable);
        }
        
        public void InvokeHit() => _hit?.OnNext(Unit.Default);
        
        public void InvokeDead() => _die?.OnNext(Unit.Default);
        
        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}