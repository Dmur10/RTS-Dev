using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RTSGame.InputManager;

namespace RTSGame.Player
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;
        public Transform enemyBuildings;

        public List<RTSResources.GameResource> playerResources;
        public List<RTSResources.GameResource> enemyResources;

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
            

            playerResources = new List<RTSResources.GameResource>()
        {
            new RTSResources.GameResource(RTSResources.ResourceType.Scrap, 200),
            new RTSResources.GameResource(RTSResources.ResourceType.Food, 200),
            new RTSResources.GameResource(RTSResources.ResourceType.Fuel, 50)
        };
        }
        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
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
                        pU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(name); ;

                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(name); 
                    }
                    else if (type = playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBuildingBaseStats(name);
                    }


                    //upgrades?
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
    }

}
