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
        private BasicUnit Worker, Warrior, Scavenger;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            pUnitLayer = LayerMask.NameToLayer("PlayerLayer");
            eUnitLayer = LayerMask.NameToLayer("EnemyLayer");

        }

        public UnitStatTypes.Base GetUnitBaseStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "worker":
                    unit = Worker;
                    break;
                case "warrior":
                    unit = Warrior;
                    break;
                case "scavenger":
                    unit = Scavenger;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} not found");
                    return null;
            }
            return unit.baseStats;
        }

        
    }
}

