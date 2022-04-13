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
        private BasicUnit Workers, Warriors, Scavengers, Engineers, Infantrys, Scouts, Raiders, Snipers, FuelRaiders;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        public void ApplyFuelPenalty(float penalty)
        {
            Raiders.baseStats.damage = Raiders.baseStats.damage * penalty;
            Raiders.baseStats.health = Raiders.baseStats.health * penalty;
            Raiders.baseStats.atkSpeed = Raiders.baseStats.atkSpeed * (2-penalty);
        }

        public void resetFuelPenalty()
        {
            Raiders.baseStats.damage = FuelRaiders.baseStats.damage;
            Raiders.baseStats.health = FuelRaiders.baseStats.health;
            Raiders.baseStats.atkSpeed = FuelRaiders.baseStats.atkSpeed;
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
                case "Scouts":
                    unit = Scouts;
                    break;
                case "Infantrys":
                    unit = Infantrys;
                    break;
                case "Snipers":
                    unit = Snipers;
                    break;
                case "Raiders":
                    unit = Raiders;
                    break;
                case "ERaiders":
                    unit = FuelRaiders;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} not found");
                    return null;
            }
            return unit.baseStats;
        }

        
    }
}

