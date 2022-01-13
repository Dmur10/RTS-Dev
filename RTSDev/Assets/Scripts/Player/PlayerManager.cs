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
        public List<GameResource> resources;

        private void Awake()
        {
            instance = this;
            SetBasicUnitStats(playerUnits);
            SetBasicUnitStats(enemyUnits);
        }
        // Start is called before the first frame update
        private void Start()
        {
            

            resources = new List<GameResource>()
        {
            new GameResource(ResourceType.Scrap, 200),
            new GameResource(ResourceType.Food, 200),
            new GameResource(ResourceType.Fuel, 50)
        };
        }
        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }

        public void SetBasicUnitStats(Transform type)
        {

            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = Units.UnitHandler.instance.GetUnitBaseStats(unitName);

                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit pU = unit.GetComponent<Units.Player.PlayerUnit>();

                        pU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(unitName); ;

                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyUnit eU = unit.GetComponent<Units.Enemy.EnemyUnit>();

                        eU.baseStats = Units.UnitHandler.instance.GetUnitBaseStats(unitName); ;
                    }


                    //upgrades?
                }
            }
        }
    }

}
