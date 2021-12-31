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
        private BasicUnit worker, warrior, scavenger;

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
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "scavenger":
                    unit = scavenger;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} not found");
                    return null;
            }
            return unit.baseStats;
        }

        public void SetBasicUnitStats(Transform type)
        {
            Transform pUnits = PlayerManager.instance.playerUnits;
            Transform eUnits = PlayerManager.instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach(Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetUnitBaseStats(unitName);

                    if (type == pUnits)
                    {
                        Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();

                        pU.baseStats = GetUnitBaseStats(unitName); ;

                    } else if(type == eUnits)
                    {
                        Enemy.EnemyUnit eU = unit.GetComponent<Enemy.EnemyUnit>();

                        eU.baseStats = GetUnitBaseStats(unitName); ;
                    }
                    

                    

                    //upgrades?
                }
            }
        }
    }
}

