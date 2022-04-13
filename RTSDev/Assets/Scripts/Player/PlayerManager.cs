using System.Collections.Generic;
using UnityEngine;
using RTSGame.InputManager;
using RTSGame.RTSResources;

namespace RTSGame.Player
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;
        public Transform enemyBuildings;

        public Dictionary<ResourceType,GameResource> playerResources;
        public Dictionary<ResourceType,GameResource> enemyResources;

        private bool playerFuelPenalty = false;

        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);
        }
        // Start is called before the first frame update
        private void Start()
        {


            playerResources = new Dictionary<ResourceType, GameResource>()
        {
            [ResourceType.Food] = new GameResource(ResourceType.Food, 400),
            [ResourceType.Scrap] = new GameResource(ResourceType.Scrap, 400),
            [ResourceType.Fuel] = new GameResource(ResourceType.Fuel, 5)
        };
        }
        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
            if ( !playerFuelPenalty && playerResources[ResourceType.Fuel].GetAmount() < 0 )
            {
                playerFuelPenalty = true;
                Units.UnitHandler.instance.ApplyFuelPenalty(0.8f);
            }

            if( playerFuelPenalty && playerResources[ResourceType.Fuel].GetAmount() >= 0)
            {
                playerFuelPenalty = false;
                Units.UnitHandler.instance.resetFuelPenalty();
            }
        }

        public bool SpendResource(float[] cost)
        {
            if (((cost[(int)ResourceType.Scrap] == 0f) || (playerResources[ResourceType.Scrap].GetAmount() >= cost[(int)ResourceType.Scrap]))
                && ((cost[(int)ResourceType.Food] == 0f) || (playerResources[ResourceType.Food].GetAmount() >= cost[(int)ResourceType.Food]))
                && (( cost[(int)ResourceType.Fuel] == 0f) || (playerResources[ResourceType.Fuel].GetAmount() >= cost[(int)ResourceType.Fuel])))
            {
                playerResources[ResourceType.Scrap].RemoveAmount(cost[(int)ResourceType.Scrap]);
                playerResources[ResourceType.Food].RemoveAmount(cost[(int)ResourceType.Food]);
                playerResources[ResourceType.Fuel].RemoveAmount(cost[(int)ResourceType.Fuel]);
                return true;
            }
            return false;
        }

        public void SetBasicStats(Transform type)
        {

            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name;

                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(name);

                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(name.Substring(1,name.Length-1)); 
                    }
                    else if (type = playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBuildingBaseStats(name);
                    }
                }
            }
        }

        public Transform GetClosestStorage(Vector3 position)
        {
            Transform closest = null;
            
            foreach (Transform tf in playerBuildings)
            {
                if(tf.name == "Headquarters" || tf.name == "Resourcehut")
                {
                    foreach(Transform resourceStore in tf)
                    {
                        if(closest == null)
                        {
                            closest = resourceStore;
                        } else
                        {
                            if(Vector3.Distance(position, resourceStore.position) < Vector3.Distance(position, closest.position))
                            {
                                closest = resourceStore;
                            }
                        }
                    }
                }
            }
            return closest;
        }

        public bool HasPlayerLost()
        {
            foreach(Transform child in playerUnits)
            {
                if(child.childCount != 0)
                {
                    return false;
                }
            }

            foreach(Transform child in playerBuildings)
            {
                if(child.childCount != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
