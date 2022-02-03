using UnityEngine;

namespace StrategyPattern
{
    public class LightParticle : MonoBehaviour, ITargetable, IMovable
    {
        [Range(1f,25f)]
        [SerializeField] private float _movableSpeed;
        [Space(5)] 
        [SerializeField] private AnimationCurve _animationCurve;
        
        private IMovementBehavior _movementBehavior;
        private Transform _currentPoint;

        public Transform MyTransform => transform;
        
        private void Awake()
        {
            _movementBehavior = new LineMovementBehavior(transform,_movableSpeed);
        }
        private void Update()
        {
            if (_currentPoint != null)
            {
                _movementBehavior.Move(_currentPoint.position);
            }
        }
        
        
        public void ChangeMovement()
        {
            if (_movementBehavior is LineMovementBehavior)
            {
                _movementBehavior = new SineMovementBehavior(transform,_animationCurve,_movableSpeed);
            }
            else if (_movementBehavior is SineMovementBehavior)
            {
                _movementBehavior = new LineMovementBehavior(transform,_movableSpeed);
            }
        }
        
        public void UpdateTarget(Transform target) => _currentPoint = target;

    }
}
