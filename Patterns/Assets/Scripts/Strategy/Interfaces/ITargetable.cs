using UnityEngine;

namespace StrategyPattern
{
    public interface ITargetable
    {
        Transform MyTransform { get; }
        void UpdateTarget(Transform target);
    }
}