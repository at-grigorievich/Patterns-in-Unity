using UnityEngine;

namespace StrategyPattern
{
    /// <summary>
    /// Context class.
    /// stores a reference to the object of a specific strategy
    /// working with it through the common strategy interface (IMovementBehavior).
    /// </summary>
    public class LightParticle : MonoBehaviour, ITargetable, IMovable
    {
        [Range(1f,25f)]
        [SerializeField] private float _movableSpeed;
        [Space(5)] 
        [SerializeField] private AnimationCurve _animationCurve;
        
        // specific strategy object
        private IMovementBehavior _movementBehavior;
        
        private Transform _currentPoint;

        public Transform MyTransform => transform;
        
        private void Awake()
        {
            // init line movement algorithm
            _movementBehavior = new LineMovementBehavior(transform,_movableSpeed);
        }
        private void Update()
        {
            if (_currentPoint != null)
            {
                // invoke the current algorithm 
                _movementBehavior.Move(_currentPoint.position);
            }
        }
        
        
        // Change movement algorithms at runtime
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
