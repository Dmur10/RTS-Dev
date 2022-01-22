using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTSGame.Buildings
{
    public enum BuildingPlacement
    {
        VALID,
        INVALID,
        FIXED
    };

    public class BuildingPlacer : MonoBehaviour
    {

        public static BuildingPlacer instance = null;
        private BasicBuilding basicBuilding = null;
        private GameObject buildingToPlace = null;

        private Ray ray;
        private RaycastHit raycastHit;
        private Vector3 _lastPlacementPosition;
        public bool isPlacing;

        private BuildingPlacement Placement = BuildingPlacement.VALID;
        private Transform _transform;
        private List<Material> _materials;
        public bool HasValidPlacement = true;

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
                    //????IsValid;???? what is this for?
                }
                SetPosition(raycastHit.point);
            }

            if (HasValidPlacement && Input.GetMouseButtonDown(0))
            {
                Place();
                buildingToPlace = null;
                //SelectBuildingToPlace(buildingToPlace.DataIndex);
            }
        }

        //click button calls this and sets type of building we are placing so get that from button name
        public void SelectBuildingToPlace(string type)
        {

            if (buildingToPlace != null && !IsFixed)
            {
                Destroy(buildingToPlace.gameObject);
            }

            //need to instantiate playerbuilding of correct type here use building
            basicBuilding = BuildingHandler.instance.GetBasicBuilding(type);
            buildingToPlace = GameObject.Instantiate(basicBuilding.buildingPrefab);
            _transform = buildingToPlace.transform;

            Buildings.Player.PlayerBuilding pb = buildingToPlace.GetComponent<Buildings.Player.PlayerBuilding>();
            pb.transform.SetParent(GameObject.Find(pb.buildingType.type.ToString()).transform);

            /*GameObject g = GameObject.Instantiate(
                Resources.Load($"Prefabs/Human/{_bluePrint.Code}")
            ) as GameObject;
            _transform = g.transform;*/
            _materials = new List<Material>();
            foreach (Material material in _transform.Find("Mesh").GetComponent<Renderer>().materials)
            {
                _materials.Add(new Material(material));
            }

            SetMaterials();
            /*Building building = new Building(
                Globals.BUILDING_DATA[index]
            );*/
            // buildingToPlace = building;


            _lastPlacementPosition = Vector3.zero;
        }

        void CancelBuildingPlacement()
        {
            Destroy(buildingToPlace.gameObject);
            buildingToPlace = null;
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public bool IsValid { get => Placement == BuildingPlacement.VALID; }
        
        public void Place()
        {
            // set placement state
            Placement = BuildingPlacement.FIXED;
            // remove "is trigger" flag from box collider to allow
            // for collisions with units
            _transform.GetComponent<BoxCollider>().isTrigger = false;
            SetMaterials();
        }

        public bool IsFixed { get => Placement == BuildingPlacement.FIXED; }

        public void SetMaterials() { SetMaterials(Placement); }

        public void SetMaterials(BuildingPlacement placement)
        {
            List<Material> materials;
            if (placement == BuildingPlacement.VALID)
            {
                Material refMaterial = Resources.Load("Materials/Valid") as Material;
                materials = new List<Material>();
                for (int i = 0; i < _materials.Count; i++)
                {
                    materials.Add(refMaterial);
                }
            }
            else if (placement == BuildingPlacement.INVALID)
            {
                Material refMaterial = Resources.Load("Materials/Invalid") as Material;
                materials = new List<Material>();
                for (int i = 0; i < _materials.Count; i++)
                {
                    materials.Add(refMaterial);
                }
            }
            else if (placement == BuildingPlacement.FIXED)
            {
                materials = _materials;
            }
            else
            {
                return;
            }
            buildingToPlace.transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
        }
    }
}

