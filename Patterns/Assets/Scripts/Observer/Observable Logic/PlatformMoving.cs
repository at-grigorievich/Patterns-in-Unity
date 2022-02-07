using UnityEngine;

namespace ObserverPattern
{
    public class PlatformMoving
    {
        private readonly float _movementSpeed;

        private readonly Transform _transform;
        private readonly Material _transformMaterial;
        
        private readonly Vector2 _maxAndMin;

        private readonly Color _defaultColor;
        
        public PlatformMoving(Transform platform,Vector2 maxAndMin,float speed)
        {
            _movementSpeed = speed;
            _transform = platform;

            _maxAndMin = maxAndMin;

            if (platform.TryGetComponent(out Renderer renderer))
            {
                _transformMaterial = renderer.material;
                _defaultColor = _transformMaterial.color;
            }
        }

        public void MoveUp()
        {
            if (_transform.position.y <= _maxAndMin[0])
            {
                _transform.position += Vector3.up * _movementSpeed * Time.deltaTime;
            }

            if (_transformMaterial != null)
            {
                _transformMaterial.color = _defaultColor;
            }
        }
        public void MoveDown()
        {
            if (_transform.position.y >= _maxAndMin[1])
            {
                _transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
            }
            else if (_transformMaterial != null)
            {
                _transformMaterial.color = Color.green;
            }
        }
    }
}