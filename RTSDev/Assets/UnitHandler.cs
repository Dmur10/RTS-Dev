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

        public (float cost, float aggroRange, float damage, float atkRange, float health) GetBasicUnitStats(string type)
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
                    return (0, 0, 0, 0, 0);
            }
            return (unit.baseStats.cost, unit.baseStats.aggroRange, unit.baseStats.damage, unit.baseStats.atkRange, unit.baseStats.health);
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
                    var stats = GetBasicUnitStats(unitName);

                    if (type == pUnits)
                    {
                        Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();

                        pU.baseStats.cost = stats.cost;
                        pU.baseStats.aggroRange = stats.aggroRange;
                        pU.baseStats.damage = stats.damage;
                        pU.baseStats.atkRange = stats.atkRange;
                        pU.baseStats.health = stats.health;

                    } else if(type == eUnits)
                    {
                        Enemy.EnemyUnit eU = unit.GetComponent<Enemy.EnemyUnit>(); 

                        eU.baseStats.cost = stats.cost;
                        eU.baseStats.aggroRange = stats.aggroRange;
                        eU.baseStats.damage = stats.damage;
                        eU.baseStats.atkRange = stats.atkRange;
                        eU.baseStats.health = stats.health;
                    }
                    

                    

                    //upgrades?
                }
            }
        }
    }
}

