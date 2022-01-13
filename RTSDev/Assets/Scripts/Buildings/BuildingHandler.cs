using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField]
        private BasicBuilding Barracks;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetUnitBaseStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "barracks":
                    building = Barracks;
                    break;
                default:
                    Debug.Log($"Building Type: {type} not found");
                    return null;
            }
            return building.baseStats;
        }
    }
}

