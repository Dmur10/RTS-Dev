using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.RTSResources
{
    public class GameResource
    {
        private ResourceType _type;
        private float _currentAmount;

        public GameResource(ResourceType type, float initialAmount)
        {
            _type = type;
            _currentAmount = initialAmount;
        }

        public float GetAmount()
        {
            return _currentAmount;
        }

        public void AddAmount(float value)
        {
            _currentAmount += value;
        }

        public void RemoveAmount(float value)
        {
            _currentAmount -= value;
        }
    }
}

