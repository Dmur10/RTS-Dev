using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSGame.Player;

namespace RTSGame.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit Workers, Warriors, Scavengers, Engineers;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        public UnitStatTypes.Base GetUnitBaseStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "Workers":
                    unit = Workers;
                    break;
                case "Warriors":
                    unit = Warriors;
                    break;
                case "Scavengers":
                    unit = Scavengers;
                    break;
                case "Engineers":
                    unit = Engineers;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} not found");
                    return null;
            }
            return unit.baseStats;
        }

        
    }
}

