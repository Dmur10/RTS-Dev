using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.RTSResources
{
    public class GameResource
    {
        private ResourceType _type;
        private int _currentAmount;

        public GameResource(ResourceType type, int initialAmount)
        {
            _type = type;
            _currentAmount = initialAmount;
        }

        public int GetAmount()
        {
            return _currentAmount;
        }

        public void AddAmount(int value)
        {
            _currentAmount += value;
        }

        public void RemoveAmount(int value)
        {
            _currentAmount -= value;
        }
    }
}

