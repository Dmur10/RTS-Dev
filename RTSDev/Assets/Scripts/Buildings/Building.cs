using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class Building : MonoBehaviour
    {
        public BasicBuilding buildingType;

        [HideInInspector]
        public BuildingStatTypes.Base baseStats;

        public BuildingStatDisplay statDisplay;
    }
}
