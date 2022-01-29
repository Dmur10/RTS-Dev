using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class BuildingZone : MonoBehaviour
    {
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
    }
}

