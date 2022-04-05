using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RTSGame.UI.HUD
{
    public class BuildBuilding : MonoBehaviour
    {
        public Buildings.BasicBuilding basicBuilding;
        public string warning;


        public void displayToolTip()
        {
            ToolTipBox.ShowToolTip_Static(basicBuilding.name, basicBuilding.baseStats.cost[0], basicBuilding.baseStats.cost[1], basicBuilding.baseStats.cost[2]);
        }

        public void unDisplayToolTip()
        {
            ToolTipBox.HideToolTip_Static();
            ToolTipWarning.HideToolTip_Static();
        }

        public void OnClick()
        {
            Debug.Log(name);
            if (Player.PlayerManager.instance.SpendResource(basicBuilding.baseStats.cost))
            {
                Buildings.BuildingPlacer.instance.SelectBuildingToPlace(basicBuilding.name);
            }
            else
            {
                ToolTipWarning.ShowToolTip_Static(warning);
            }
        }
    }
}
