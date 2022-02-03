using UnityEngine;

namespace StrategyPattern
{
    public class SineMovementBehavior: IMovementBehavior
    {
        private readonly Transform _transform;
        private readonly float _speed;

        private AnimationCurve _curve;

        private static float _curTime;

        public SineMovementBehavior(Transform transform, AnimationCurve curve,float speed)
        {
            _speed = speed;
            _transform = transform;

            _curve = curve;
            _curve.postWrapMode = WrapMode.Loop;
        }
        
        public void Move(Vector3 targetPosition)
        {
            Vector3 currentPosition = _transform.position;
            currentPosition.y = targetPosition.y;

            Vector3 nextPosition = 
                Vector3.MoveTowards(
                    currentPosition, 
                    targetPosition, 
                    _speed * Time.deltaTime);

            nextPosition.y = _curve.Evaluate(_curTime);
            _transform.position = nextPosition;


            _curTime += Time.deltaTime;
        }
    }
}