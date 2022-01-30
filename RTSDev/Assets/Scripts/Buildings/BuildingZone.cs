using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class BuildingZone : MonoBehaviour
    {

        /*public static BuildingZone Create(Vector3 position, Vector3 size,)
        {
            Transform buildingZoneTransform
            BuildingZone buildingZone =
        }*/

        private int constructionTick;
        private int maxConstructionTick;
        public GameObject prefab;
        private Action onConstructionComplete;

        public void AddConstructionTick()
        {
            constructionTick++;

            if(constructionTick > maxConstructionTick)
            {
                onConstructionComplete();
                Destroy(gameObject);
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsBuilt()
        {
            return constructionTick >= maxConstructionTick;
        }
    }
}

