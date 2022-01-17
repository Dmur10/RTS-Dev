using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IBuilding : Interactable
    {
        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;
        public override void OnInteractEnter()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions,spawnMarker); 
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            base.OnInteractExit();
        }

        public void SetSpawnMarkerLocation()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                spawnMarker.transform.position = hit.point;
            }
        }
    }
}

