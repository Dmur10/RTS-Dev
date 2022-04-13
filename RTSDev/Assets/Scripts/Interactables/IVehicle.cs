using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Interactables
{
    public class IVehicle : MonoBehaviour
    {

        // Update is called once per frame
        private void  Update()
        {
            Player.PlayerManager.instance.playerResources[RTSResources.ResourceType.Fuel].RemoveAmount(Time.deltaTime);
        }
    }
}

