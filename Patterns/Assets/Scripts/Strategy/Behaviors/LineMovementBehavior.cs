using UnityEngine;
using UnityEngine.Jobs;

namespace StrategyPattern
{
    /// <summary>
    /// Linear movement algorithm
    /// </summary>
    public class LineMovementBehavior: IMovementBehavior
    {
        private readonly Transform _transform;
        private readonly float _speed;
        
        public LineMovementBehavior(Transform transform,float speed)
        {
            _speed = speed;
            _transform = transform;
        }
        
        public void Move(Vector3 targetPosition)
        {
            _transform.position = 
                Vector3.MoveTowards(
                    _transform.position, 
                    targetPosition, 
                    _speed * Time.deltaTime);
        }
    }
}