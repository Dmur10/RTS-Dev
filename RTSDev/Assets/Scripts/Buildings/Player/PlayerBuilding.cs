using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings.Player
{
    public class PlayerBuilding : MonoBehaviour
    {

        public BasicBuilding buildingType;

        [HideInInspector]
        public BuildingStatTypes.Base baseStats;

        public BuildingStatDisplay statDisplay;

        

        private void Start()
        {
            baseStats = buildingType.baseStats;
            statDisplay.SetStatDisplayBasicBuilding(baseStats, true);
        }

        
    }
}

