using UnityEngine;

namespace StrategyPattern
{
    public interface IMovementBehavior
    {
        void Move(Vector3 targetPosition);
    }
}