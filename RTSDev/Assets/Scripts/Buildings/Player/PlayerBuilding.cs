using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings.Player
{
    public enum BuildingPlacement
    {
        VALID,
        INVALID,
        FIXED
    };

    public class PlayerBuilding : MonoBehaviour
    {

        public BasicBuilding buildingType;

        [HideInInspector]
        public BuildingStatTypes.Base baseStats;

        public Units.UnitStatDisplay statDisplay;

        private BuildingPlacement _placement = BuildingPlacement.VALID;
        private Transform _transform;
        private List<Material> _materials;
        public bool HasValidPlacement = true;

        private void Start()
        {
            baseStats = buildingType.baseStats;
            //statDisplay.SetStatDisplayBasicBuilding(baseStats, true);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public Transform Transform { get => _transform; }

        public bool CheckValidPlacement()
        {
            if (_placement == BuildingPlacement.VALID)
            {
                return true;
            }
            return false;
        }

        public void Place()
        {
            // set placement state
            _placement = BuildingPlacement.FIXED;
            // remove "is trigger" flag from box collider to allow
            // for collisions with units
            _transform.GetComponent<BoxCollider>().isTrigger = false;
            SetMaterials();
        }

        public bool IsFixed { get => _placement == BuildingPlacement.FIXED; }

        public void SetMaterials() { SetMaterials(_placement); }

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
            _transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
        }
    }
}

