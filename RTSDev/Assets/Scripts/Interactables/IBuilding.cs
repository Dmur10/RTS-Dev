using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IBuilding : Interactable
    {
        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;
        public GameObject spawnMarkerGraphic = null;

        public override void OnInteractEnter()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions,spawnMarker);
            spawnMarkerGraphic.SetActive(true);
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            spawnMarkerGraphic.SetActive(false);
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

