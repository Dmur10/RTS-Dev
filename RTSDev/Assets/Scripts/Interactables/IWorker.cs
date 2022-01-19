using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IWorker : IUnit
    {

        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
            UI.HUD.ActionFrame.instance.ClearActions();
        }

        public void OnBuilderSelect()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, spawnMarker);
        }
    }
}

