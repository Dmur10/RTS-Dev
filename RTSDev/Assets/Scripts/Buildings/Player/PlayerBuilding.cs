using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings.Player
{
    public class PlayerBuilding : Building
    {
        private void Start()
        {
            baseStats = buildingType.baseStats;
            statDisplay.SetStatDisplayBasicBuilding(baseStats, true);
        }
    }
}

