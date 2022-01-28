using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBluePrint : MonoBehaviour
{
    private float buildTime;
    public GameObject prefab;

    BuildingBluePrint(float spawnTime)
    {
        buildTime = spawnTime;
    }
    private void Update()
    {
        buildTime -= Time.deltaTime;
    }
}
