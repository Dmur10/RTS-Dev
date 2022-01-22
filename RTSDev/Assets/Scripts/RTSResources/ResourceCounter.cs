using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.RTSResources
{
    public class ResourceCounter : MonoBehaviour
    {
        public Text resourceAmt;
        public ResourceType type;

        public void Update()
        {
            resourceAmt.text = RTSGame.Player.PlayerManager.instance.resources[(int)type].GetAmount().ToString();
        }
    }
}

