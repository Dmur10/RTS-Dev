using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        public void ShowToolTip()
        {
            ToolTip.ShowToolTip_Static(button.name);
        }

        public void HideToolTip()
        {
            ToolTip.HideToolTip_Static();
        }
    }
}
