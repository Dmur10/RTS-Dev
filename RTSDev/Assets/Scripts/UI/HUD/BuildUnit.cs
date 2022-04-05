using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RTSGame.UI.HUD
{
    public class BuildUnit : MonoBehaviour
    {
        public Units.BasicUnit basicUnit;
        public string warning;

      
        public void displayToolTip()
        {
            ToolTipBox.ShowToolTip_Static(basicUnit.name, basicUnit.baseStats.cost[0], basicUnit.baseStats.cost[1], basicUnit.baseStats.cost[2]);
        }


        public void unDisplayToolTip()
        {
            ToolTipWarning.HideToolTip_Static();
            ToolTipBox.HideToolTip_Static();
        }

        public void OnClick()
        {
            Debug.Log(name);
            if (Player.PlayerManager.instance.SpendResource(basicUnit.baseStats.cost))
            {
                Buildings.ProductionHandler.instance.AddToQueue(name);
            }
            else
            {
                ToolTipWarning.ShowToolTip_Static(warning);
            }
        }
    }

}
