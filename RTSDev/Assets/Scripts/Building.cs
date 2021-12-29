using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuildingPlacement
{
    VALID,
    INVALID,
    FIXED
};
public class Building
{

    private BuildingPlacement _placement = BuildingPlacement.VALID;
    private BuildingBluePrint _bluePrint;
    private Transform _transform;
    private int _currentHealth;

    private List<Material> _materials;
    public bool HasValidPlacement = true;

    public Building(BuildingBluePrint bluePrint)
    {
        _bluePrint = bluePrint;
        _currentHealth = bluePrint.Health;

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


    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public string Code { get => _bluePrint.Code; }
    public Transform Transform { get => _transform; }
    public int HP { get => _currentHealth; set => _currentHealth = value; }

    public bool CheckValidPlacement()
    {

        if (_placement == BuildingPlacement.VALID)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int MaxHP { get => _bluePrint.Health; }
    public int DataIndex
    {
        get
        {
            for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
            {
                if (Globals.BUILDING_DATA[i].Code == _bluePrint.Code)
                {
                    return i;
                }
            }
            return -1;
        }
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
}
