using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    private ResourceType _type;
    private int _currentAmount;

    public GameResource(ResourceType type, int initialAmount)
    {
        _type = type;
        _currentAmount = initialAmount;
    }

    public void AddAmount(int value)
    {
        _currentAmount += value;
        if (_currentAmount < 0)
        {
            _currentAmount = 0;

        }
    }
}
