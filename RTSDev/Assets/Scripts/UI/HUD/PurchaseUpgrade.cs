using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RTSGame.UI.HUD
{
    public class PurchaseUpgrade: MonoBehaviour
    {
        public Upgrades.BasicUpgrade basicUpgrade;
        public string warning;

        public void displayToolTip()
        {
            ToolTipBox.ShowToolTip_Static(basicUpgrade.name, basicUpgrade.cost[0], basicUpgrade.cost[1], basicUpgrade.cost[2]);
        }

        public void unDisplayToolTip()
        {
            ToolTipBox.HideToolTip_Static();
            ToolTipWarning.HideToolTip_Static();
        }

        public void OnClick()
        {
            Debug.Log(name);
            if (Player.PlayerManager.instance.SpendResource(basicUpgrade.cost))
            {
                ActionFrame.instance.Upgrade(basicUpgrade.name);
            }
            else
            {
                ToolTipWarning.ShowToolTip_Static(warning);
            }
        }
    }
}
