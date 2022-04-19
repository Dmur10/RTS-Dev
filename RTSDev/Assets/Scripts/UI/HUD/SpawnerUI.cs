using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class SpawnerUI : MonoBehaviour
    {
        [SerializeField]
        private Text WaveInfo;

        private void Update()
        {
            WaveInfo.text = Spawning.SpawnHandler.instance.GetWaveInfo();
        }
    }
}
