using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTSGame.Buildings
{
    public class BuildingPlacer : MonoBehaviour
    {

        public BuildingPlacer instance = null;
        private Player.PlayerBuilding buildingToPlace = null;
        private GameObject building = null;

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
            if (buildingToPlace == null)
            {
                return;
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuildingPlacement();
                return;
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 1000f, Globals.TERRAIN_LAYER_MASK))
            {

                if (_lastPlacementPosition != raycastHit.point)
                {
                    buildingToPlace.CheckValidPlacement();
                }
                buildingToPlace.SetPosition(raycastHit.point);
            }

            if (buildingToPlace.HasValidPlacement && Input.GetMouseButtonDown(0))
            {
                buildingToPlace.Place();
                buildingToPlace = null;
                //SelectBuildingToPlace(buildingToPlace.DataIndex);
            }
        }

        //click button calls this and sets type of building we are placing so get that from button name
        public void SelectBuildingToPlace(string type)
        {

            if (buildingToPlace != null && !buildingToPlace.IsFixed)
            {
                Destroy(buildingToPlace.Transform.gameObject);
            }

            //need to instantiate playerbuilding of correct type here use building
            GameObject g = GameObject.Instantiate(
                Resources.Load($"Prefabs/Human/{_bluePrint.Code}")
            ) as GameObject;
            _transform = g.transform;

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
}

