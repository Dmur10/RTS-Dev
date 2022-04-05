using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IStorage : IBuilding
    {
        public void StoreResource(float amount, RTSResources.ResourceType rType)
        {
            if(gameObject.GetComponent<Buildings.Player.PlayerBuilding>())
            {
                Player.PlayerManager.instance.playerResources[rType].AddAmount(amount);
            }
        }
    }
}

