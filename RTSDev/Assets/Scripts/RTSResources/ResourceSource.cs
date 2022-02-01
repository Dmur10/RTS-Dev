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
        public float quantity = 10;

        public void DecrementResource()
        {
            if(quantity > 1)
            {
                quantity--;
            } else
            {
                RemoveResource();
            }
        }

        public ResourceType GetResourceType()
        {
            return type;
        }

        private void RemoveResource()
        {
            Destroy(gameObject);
        }
    }
}

