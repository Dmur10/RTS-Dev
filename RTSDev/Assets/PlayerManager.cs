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
 
        public Camera cam;
        public NavMeshAgent agent;
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

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        void moveSelectedUnits()
        {
             
        }
    }

}
