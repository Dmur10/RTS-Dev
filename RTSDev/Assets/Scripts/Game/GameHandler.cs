using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Game
{
    public class GameHandler : MonoBehaviour
    {

        public static GameHandler instance;
        public Transform buildingZone;
        [SerializeField]
        private Transform WinScreen;
        [SerializeField]
        private Transform LossScreen;
        private bool GameOver;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Spawning.SpawnHandler.instance.AllWavesCompleted())
            {
                GameOver = true;
                Pause();
                WinScreen.gameObject.SetActive(true);
            } else if (Player.PlayerManager.instance.HasPlayerLost())
            {
                GameOver = true;
                Pause();
                LossScreen.gameObject.SetActive(true);
            }
        }

        public bool IsGameOver()
        {
            return GameOver;
        }

        private void Pause()
        {
            Time.timeScale = 0f;
        }

    }
}


