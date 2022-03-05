using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.UI.HUD
{
    public class UpgradeUnitAction : MonoBehaviour
    {
        public void OnClick()
        {
            ActionFrame.instance.Upgrade(name);
        }
    }
}
