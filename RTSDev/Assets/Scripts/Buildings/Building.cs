using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTSGame.Buildings
{
    
    public class Building
    {

        public Building(BuildingBluePrint bluePrint)
        {
            _bluePrint = bluePrint;

            GameObject g = GameObject.Instantiate(
                Resources.Load($"Prefabs/Human/{_bluePrint.Code}")
            ) as GameObject;
            _transform = g.transform;

            _materials = new List<Material>();
            foreach (Material material in _transform.Find("Mesh").GetComponent<Renderer>().materials)
            {
                _materials.Add(new Material(material));
            }

            SetMaterials();

        }
    }

}
