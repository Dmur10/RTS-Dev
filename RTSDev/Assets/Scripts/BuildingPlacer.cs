using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingPlacer : MonoBehaviour
{

    public BuildingPlacer instance = null;
    private Building buildingToPlace = null;

    private Ray ray;
    private RaycastHit raycastHit;
    private Vector3 _lastPlacementPosition;
    public bool isPlacing;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    private void Update()
    {
        if(buildingToPlace == null)
        {
            return;
        }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuildingPlacement();
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
                buildingToPlace.Place();
                SelectBuildingToPlace(buildingToPlace.DataIndex);
            }
    }

    public void SelectBuildingToPlace(int index) { 

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

    void CancelBuildingPlacement()
    {
        Destroy(buildingToPlace.Transform.gameObject);
        buildingToPlace = null;
    }
}
