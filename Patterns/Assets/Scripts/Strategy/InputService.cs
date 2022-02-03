using System;
using UnityEngine;

namespace StrategyPattern
{
    public class InputService: MonoBehaviour
    {
        private IMovable _movableObject;

        private void Awake()
        {
            _movableObject = FindObjectOfType<LightParticle>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movableObject.ChangeMovement();
            }
        }
    }
}