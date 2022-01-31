using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Game
{
    public class GameHandler : MonoBehaviour
    {

        public static GameHandler instance;
        public Transform buildingZone;

        private void Awake()
        {
            instance = this;
        }

        
    }
}


