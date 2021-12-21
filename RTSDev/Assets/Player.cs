using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public static Player instance;
    public List<GameResource> resources;

    public List<Transform> selectedUnits;
    public List<Transform> units;

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
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if( Physics.Raycast(ray, out hit) )
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void moveSelectedUnits()
    {
        if(selectedUnits.Count == 1)
        {
// selectedUnits[0];
        } else
        {

        }
    }
}
