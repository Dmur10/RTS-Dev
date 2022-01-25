using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    [CreateAssetMenu(fileName = "New Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum BuildingType
        {
            Headquarters,
            House,
            Barracks,
            ResourceGather,
            Defense
        }

        [Space(15)]
        [Header("Buildig Settngs")]

        public BuildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public float SpawnTime;

        [Space(15)]
        [Header("Buildig Base Stats")]
        [Space(40)]

        public BuildingStatTypes.Base baseStats;
    }
}

