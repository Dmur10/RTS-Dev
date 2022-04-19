using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public void ShowToolTip()
        {
            ToolTip.ShowToolTip_Static(image.name);
        }

        public void HideToolTip()
        {
            ToolTip.HideToolTip_Static();
        }
    }
}
