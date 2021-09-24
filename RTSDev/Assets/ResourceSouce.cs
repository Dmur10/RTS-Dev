using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Scrap,
    Food,
    Fuel
}
public class ResourceSource : MonoBehaviour
{
    public ResourceType type;
    public int quantity;

    public void GatherResource(int amount)
    {
        if (quantity - amount < 0)
        {
            quantity = 0;
        }
        else
        {
            quantity -= amount;
        }
    }
}
