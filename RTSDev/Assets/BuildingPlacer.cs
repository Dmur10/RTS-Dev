using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{

    private Building buildingToPlace = null;

    private Ray ray;
    private RaycastHit raycastHit;
    private Vector3 _lastPlacementPosition;

    // Start is called before the first frame update
    void Start()
    {
        selectBuildingToPlace(0);
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
            if (Physics.Raycast( ray, out raycastHit, 1000f, Globals.TERRAIN_LAYER_MASK)) {

                if (_lastPlacementPosition != raycastHit.point)
                {
                    buildingToPlace.CheckValidPlacement();
                }
                buildingToPlace.SetPosition(raycastHit.point);
            }

            if (buildingToPlace.HasValidPlacement && Input.GetMouseButtonDown(0))
            {
                print("here");
                buildingToPlace.Place();
                selectBuildingToPlace(buildingToPlace.DataIndex);
            }
        }
    }

    void selectBuildingToPlace(int index) { 

        if (buildingToPlace != null && !buildingToPlace.IsFixed)
        {
            Destroy(buildingToPlace.Transform.gameObject);
        }
        Building building = new Building(
            Globals.BUILDING_DATA[index]
        );
        buildingToPlace = building;
        _lastPlacementPosition = Vector3.zero;
    }

    void cancelBuildingPlacement()
    {
        Destroy(buildingToPlace.Transform.gameObject);
        buildingToPlace = null;
    }
}
