using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Buildings
{
    public class ProductionHandler : MonoBehaviour
    {
        public static ProductionHandler instance = null;

        [SerializeField] private BuildingProductionQueue buildingProductionQueue = null;
        [SerializeField] private UI.HUD.ProductionQueueUI productionQueueUI = null;

        [SerializeField]
        private Image progressBar;

        private void Awake()
        {
            instance = this;
        }

        public void AddToQueue(string name)
        {
            if (buildingProductionQueue.StartSpawnTimer(name))
            {
                productionQueueUI.AddToQueue();
            }
        }

        public void RemoveFromQueue()
        {
            productionQueueUI.FillQueue(buildingProductionQueue.getQueue());
        }

        public void SetProductionQueue(BuildingProductionQueue buildingProductionQueue)
        {
            this.buildingProductionQueue = buildingProductionQueue;
            productionQueueUI.FillQueue(buildingProductionQueue.getQueue());
        }
    }
}

