using System;
using UnityEngine;

namespace StrategyPattern
{
    public class CheckDistanceService: MonoBehaviour
    {
        [SerializeField] private Transform _fTarget;
        [SerializeField] private Transform _sTarget;

        private ITargetable _movableObject;

        private Transform _currentTarget;

        
        private void Awake()
        {
            _movableObject = FindObjectOfType<LightParticle>();

            if (_movableObject == null)
                throw new NullReferenceException("Cant find movable object");
            
            ChangeTarget();
        }

        private void Update()
        {
            Vector3 tarPos = _movableObject.MyTransform.position;
            if (Vector3.Distance(tarPos, _currentTarget.position) <= 0.1f)
            {
                ChangeTarget();
            }
        }

        private void ChangeTarget()
        {
            if (ReferenceEquals(_currentTarget, _fTarget))
            {
                _currentTarget = _sTarget;
            }
            else
            {
                _currentTarget = _fTarget;
            }
            
            _movableObject.UpdateTarget(_currentTarget);
        }
    }
}