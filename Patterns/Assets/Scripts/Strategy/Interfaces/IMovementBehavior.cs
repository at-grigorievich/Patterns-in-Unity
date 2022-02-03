using UnityEngine;

namespace StrategyPattern
{
    /// <summary>
    /// Сommon interface  for all variations of the movement algorithm.
    /// The context uses this interface to call the algorithm
    /// </summary>
    public interface IMovementBehavior
    {
        void Move(Vector3 targetPosition);
    }
}