using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IFuelStation : IBuilding
    {
        private bool captured = false;

        private void Update()
        {
            if (captured)
            {
                Player.PlayerManager.instance.playerResources[(int)RTSResources.ResourceType.Fuel].AddAmount(Time.deltaTime);
            }
        }

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }
    }
}
