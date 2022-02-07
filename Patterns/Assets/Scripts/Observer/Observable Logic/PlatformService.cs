using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    /// <summary>
    /// Data provider. Sends the number of clicks to observers
    /// </summary>
    public class PlatformService : MonoBehaviour, IObservable<int>
    {
        [Range(.1f,5f)]
        [SerializeField] private float _movementSpeed;

        [SerializeField] private Vector2 _maxAndMin;
        [SerializeField] private float _upDelay;

        private PlatformMoving _movableService;
        
        private float _curDelay;
        
        private int _inputCount;

        private List<IObserver<int>> _observers = new List<IObserver<int>>();
        
        private void Awake()
        {
            _movableService = new PlatformMoving(transform,_maxAndMin, _movementSpeed);
            _curDelay = _upDelay;
        }

        public void Update()
        {
            if (_curDelay >= _upDelay)
            {
                _inputCount = 0;
                _movableService.MoveUp();
            }

            if (Input.GetMouseButton(0))
            {
                OnUnitClick();
                _curDelay = 0f;
            }

            _curDelay += Time.deltaTime;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private void OnUnitClick()
        {
            _inputCount++;
            foreach (var observer in _observers)
            {
                observer.OnNext(_inputCount);   
            }
            
            _movableService.MoveDown();
        }
        
        public class Unsubscriber: IDisposable
        {
            private List<IObserver<int>> _observers;
            private IObserver<int> _observer;

            public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
            {
                _observer = observer;
                _observers = observers;
            }

            public void Dispose()
            {
                if (_observer != null)
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}