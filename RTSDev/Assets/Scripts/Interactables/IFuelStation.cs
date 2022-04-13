using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IFuelStation : MonoBehaviour
    {
        private void Update()
        {
            Player.PlayerManager.instance.playerResources[RTSResources.ResourceType.Fuel].AddAmount(Time.deltaTime*4);
        }
    }
}
