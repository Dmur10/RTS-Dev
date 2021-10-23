using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public static Player instance;
    public List<Transform> selectedUnits;
    public List<Transform> units;

    public Camera cam;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
            //selectedUnits.GetComponent<NavMeshAgent>.SetDestination(hit.point);
        }
    }
}
