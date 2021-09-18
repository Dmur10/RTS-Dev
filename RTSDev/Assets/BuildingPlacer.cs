using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public Building buildingToPlace = null;
    private Ray ray;
    private RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buildingToPlace != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                cancelBuildingPlacement();
                return;
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast( ray, out raycastHit, 1000f, 8)) {

                buildingToPlace._transform.position = raycastHit.point;
                //buildingToPlace.CheckValidPlacement();
            }

            //if (buildingToPlace.HasValidPlacement && Input.GetMouseButtonDown(0))
           // {
                buildingToPlace.place();
           // }
        }
    }

    void selectBuildingToPlace(Building buildingToPlace) { 

        if (buildingToPlace != null)
        {
            Destroy(buildingToPlace._transform.gameObject);
        }

        this.buildingToPlace = buildingToPlace;
    }

    void cancelBuildingPlacement()
    {
        Destroy(buildingToPlace._transform.gameObject);
        buildingToPlace = null;
    }
}
