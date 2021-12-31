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
            
        }
        // Start is called before the first frame update
        private void Start()
        {
            Units.UnitHandler.instance.SetBasicUnitStats(playerUnits);
            Units.UnitHandler.instance.SetBasicUnitStats(enemyUnits);

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
    }

}
