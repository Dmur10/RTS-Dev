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
        private float offset;
        private GameObject Prefab;
        public Image Progress;

        public void SetUp(Vector3 size, int constructionTickMax, GameObject toBuild)
        {
            transform.localScale = size;
            offset = Vector3.Distance(transform.position, transform.Find("Corner").position);
            this.constructionTickMax = constructionTickMax;
            Prefab = toBuild;
        }

        public float GetOffset()
        {
            return offset;
        }
        public void AddConstructionTick()
        {
            constructionTick++;
            Progress.fillAmount = (float)constructionTick / constructionTickMax;

            if(constructionTick >= constructionTickMax)
            {
                Destroy(gameObject);
                Prefab = Instantiate(Prefab, transform.position, Quaternion.identity);
                Player.PlayerBuilding pb = Prefab.GetComponent<Player.PlayerBuilding>();
                pb.transform.SetParent(GameObject.Find(pb.buildingType.type.ToString()).transform);
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

