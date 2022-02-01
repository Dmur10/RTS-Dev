using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Buildings
{
    public class BuildingZone : MonoBehaviour
    {

        public static BuildingZone Create(Vector3 position, Vector3 size, int constructionTickMax, GameObject toBuild)
        {
            Transform buildingZoneTransform = Instantiate(Game.GameHandler.instance.buildingZone, position, Quaternion.identity);
            BuildingZone buildingZone = buildingZoneTransform.GetComponent<BuildingZone>();
            buildingZone.SetUp(size, constructionTickMax, toBuild);
            return buildingZone;
        }

        private int constructionTick;
        private int constructionTickMax;
        private GameObject Prefab;
        public Image Progress;

        public void SetUp(Vector3 size, int constructionTickMax, GameObject toBuild)
        {
            transform.localScale = size;
            this.constructionTickMax = constructionTickMax;
            Prefab = toBuild;
        }

        public void AddConstructionTick()
        {
            constructionTick++;
            Progress.fillAmount = (float)constructionTick / constructionTickMax;
            Debug.Log(Progress.fillAmount);

            if(constructionTick >= constructionTickMax)
            {
                Destroy(gameObject);
                Prefab = Instantiate(Prefab, transform.position, Quaternion.identity);
                Prefab.GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsBuilt()
        {
            return constructionTick >= constructionTickMax;
        }
    }
}

