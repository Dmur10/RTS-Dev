using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingBluePrint
{
    private int _cost;
    private int _health;
    private string _code;

    public BuildingBluePrint(string code, int health, int cost)
    {
        _code = code;
        _health = health;
        _cost = cost;
    }

    public int Health { get => _health; set => _health = value; }
    public int Cost { get => _cost; set => _cost = value; }
    public string Code { get => _code; set => _code = value; }
}
