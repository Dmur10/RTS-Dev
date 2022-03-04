using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTSGame.Buildings.Enemy {
    public class EnemyBuilding : Building
    {
        private void Start()
        {
            baseStats = buildingType.baseStats;
            statDisplay.SetStatDisplayBasicBuilding(baseStats, false);
        }
    }
}

