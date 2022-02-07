using System;
using UnityEngine;

namespace ObserverPattern
{
    public class UnitsContainer : MonoBehaviour
    {
        [SerializeField] private PlatformService _platformService;
        [Space(5)] [SerializeField] private UnitAgent[] _agents;

        private void Start()
        {
            foreach (var unitAgent in _agents)
            {
                unitAgent.Subscribe(_platformService);
            }
        }
    }
}