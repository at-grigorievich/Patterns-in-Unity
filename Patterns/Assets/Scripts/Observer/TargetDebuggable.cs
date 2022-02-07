using UnityEngine;

namespace ObserverPattern
{
    public class TargetDebuggable : MonoBehaviour
    {
        [SerializeField] private bool _isMainTarget;

        private void OnDrawGizmos()
        {
            Gizmos.color = _isMainTarget ? Color.green : Color.grey;

            float radius = _isMainTarget ? 2f : .5f;

            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
