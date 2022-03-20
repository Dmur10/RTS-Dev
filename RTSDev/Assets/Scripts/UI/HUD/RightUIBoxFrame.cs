using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class RightUIBoxFrame : MonoBehaviour
    {
        public static RightUIBoxFrame instance = null;
        public GameObject commandBox;
        public GameObject productionQueue;

        private void Awake()
        {
            instance = this;
        }

        public void ActivateCommandBox()
        {
            productionQueue.SetActive(false);
            commandBox.SetActive(true);
        }

        public void ActivateProductionQueue()
        {
            commandBox.SetActive(false);
            productionQueue.SetActive(true);
        }

        public void ClearRightUI()
        {
            commandBox.SetActive(false);
            productionQueue.SetActive(false);
        }
    }

}