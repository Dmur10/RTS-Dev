using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.RTSResources
{
    public enum ResourceType
    {
        Scrap,
        Food,
        Fuel
    }

    public class ResourceSource : MonoBehaviour
    {
        public ResourceType type;
        public float quantity;

        public void GatherResource(float amount)
        {
            if (quantity - amount < 0)
            {
                RemoveResource();
            }
            else
            {
                quantity -= amount;
            }
        }

        private void RemoveResource()
        {
            Destroy(gameObject);
        }
    }
}

