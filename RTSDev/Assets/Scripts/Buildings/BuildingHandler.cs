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

        public BuildingStatTypes.Base GetBuildingBaseStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "barrack":
                    building = Barracks;
                    break;
                default:
                    Debug.Log($"Building Type: {type} not found");
                    return null;
            }
            return building.baseStats;
        }

        public BasicBuilding GetBasicBuilding(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "Barracks":
                    building = Barracks;
                    break;
                default:
                    Debug.Log($"Building Type: {type} not found");
                    return null;
            }
            return building;
        }


    }
}

