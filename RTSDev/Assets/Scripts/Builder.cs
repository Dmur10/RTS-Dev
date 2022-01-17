using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Units
{
    public class Builder : MonoBehaviour
    {
        public UI.HUD.PlayerActions actions;

        public void OnBuilderSelect()
        {
            //UI.HUD.ActionFrame.instance.SetActionButtons(actions);
        }

        public void OnBuilderExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
        }
    }
}

