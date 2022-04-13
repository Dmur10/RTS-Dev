using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField] private BasicBuilding Headquarters;
        [SerializeField] private BasicBuilding Barracks;
        [SerializeField] private BasicBuilding Resourcehut;
        [SerializeField] private BasicBuilding Pillbox;
        [SerializeField] private BasicBuilding Garage;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBuildingBaseStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "Headquarters":
                    building = Headquarters;
                    break;
                case "Barracks":
                    building = Barracks;
                    break;
                case "Resourcehut":
                    building = Resourcehut;
                    break;
                case "Pillbox":
                    building = Pillbox;
                    break;
                case "Garage":
                    building = Garage;
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
                case "Headquarters":
                    building = Headquarters;
                    break;
                case "Barracks":
                    building = Barracks;
                    break;
                case "Resourcehut":
                    building = Resourcehut;
                    break;
                case "Pillbox":
                    building = Pillbox;
                    break;
                case "Garage":
                    building = Garage;
                    break;
                default:
                    Debug.Log($"Building Type: {type} not found");
                    return null;
            }
            return building;
        }


    }
}

