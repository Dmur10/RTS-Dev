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
        public List<GameResource> resources;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            resources = new List<GameResource>()
        {
            new GameResource(ResourceType.Scrap, 200),
            new GameResource(ResourceType.Food, 200),
            new GameResource(ResourceType.Fuel, 50)
        };
        }
        // Update is called once per frame
        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }

}
