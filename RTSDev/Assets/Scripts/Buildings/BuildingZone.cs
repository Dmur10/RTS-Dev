using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{ 
    public class BuildingZone : MonoBehaviour
    {
        private float buildTime;
        private bool HasBuilder;
        public GameObject prefab;

        BuildingZone(float spawnTime, GameObject prefab)
        {
            buildTime = spawnTime;
            this.prefab = prefab;
            Instantiate(this);
        }
        private void Update()
        {
            if (HasBuilder)
            {
                buildTime -= Time.deltaTime;
                if (buildTime < 0)
                {
                    BuildComplete();
                }
            }
        }

        public void AssignBuilder()
        {
            HasBuilder = true;
        }

        private void BuildComplete()
        {
            Instantiate(prefab, transform);
            Destroy(gameObject);
        }
    }
}

