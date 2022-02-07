using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ObserverPattern
{
    /// <summary>
    /// Observer monitors the number of clicks
    /// if there are more than necessary, then goes to the center of the map
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
    public class UnitAgent : MonoBehaviour, IObserver<int>
    {
        [SerializeField] private int _neededValue;
        [Space(10)] 
        [SerializeField] private Transform[] _points;
        [SerializeField] private Transform _mainPoint;
        
        private IDisposable _unsubscriber;
        
        private Queue<Transform> _pointsQueue;

        private NavMeshAgent _agent;
        private Rigidbody _rb;

        private Action _targetAction;
        
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

            _pointsQueue = new Queue<Transform>(_points);

            _targetAction = GoToNextPoint;
        }
        private void Update()
        {
            _targetAction?.Invoke();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlatformService ps))
            {
                Unsubscribe();
                transform.SetParent(ps.transform);
            }
        }

        #region Observer implementation

        public virtual void Subscribe(IObservable<int> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        public virtual void Unsubscribe()
        {
            Debug.LogWarning("Unit is on platform");

            _targetAction = null;
            _unsubscriber.Dispose();
            _agent.enabled = false;

            _rb.isKinematic = true;
        }
        
        public void OnCompleted()
        {
            //Stop moving
            Debug.LogWarning("Complete");
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(int value)
        {
            if (value >= _neededValue)
            {
                // unit move to center
                _targetAction = () => _agent.destination = _mainPoint.position;
            }
        }

        #endregion
        
        
        private void GoToNextPoint()
        {
            if (!_agent.pathPending && _agent.remainingDistance < .5f)
            {
                if (_pointsQueue.Count == 0)
                {
                    _pointsQueue = new Queue<Transform>(_points);
                }

                Transform currentTargetPoint = _pointsQueue.Dequeue();

                _agent.destination = currentTargetPoint.position;
            }
        }
    }
}
