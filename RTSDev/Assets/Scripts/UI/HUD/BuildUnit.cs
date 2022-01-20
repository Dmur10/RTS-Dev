using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.UI.HUD
{
    public class BuildUnit : MonoBehaviour
    {
        public void OnClick()
        {
            Debug.Log(name);
            ActionFrame.instance.StartSpawnTimer(name);
        }
    }

}
