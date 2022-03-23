using RTSGame.UI.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public struct SpawnableObject
    {
        public float queue;
        public Units.BasicUnit order;

        public SpawnableObject(float queue, Units.BasicUnit order)
        {
            this.queue = queue;
            this.order = order;
        }
    }
    public class BuildingProductionQueue : MonoBehaviour
    { 
        public List<SpawnableObject> spawnQueue = new List<SpawnableObject>();
        public Transform spawnPoint = null;
        public Transform flagLocation = null;

        public IEnumerator SpawnQueueTimer()
        {
            if (spawnQueue.Count > 0)
            {
                Debug.Log($"Waiting for {spawnQueue[0].queue}");
                yield return new WaitForSeconds(spawnQueue[0].queue);
                SpawnObject();
                Debug.Log("spawn");
            }

            if (spawnQueue.Count > 0)
            {
                StartCoroutine(SpawnQueueTimer());
            }
        }

        public List<SpawnableObject> getQueue()
        {
            return spawnQueue;
        }

        public bool StartSpawnTimer(string objectToSpawn)
        {
            bool ValidObject = false;
            if (IsUnit(objectToSpawn) && spawnQueue.Count < 9)
            {
                Units.BasicUnit unit = IsUnit(objectToSpawn);
                spawnQueue.Add(new SpawnableObject(unit.SpawnTime, unit));
                ValidObject = true;
            } else if (IsUpgrade(objectToSpawn))
            {
                ValidObject = true;
            }
            else
            {
                Debug.Log($"{objectToSpawn} is not spawnable");
                ValidObject = false;
            }

            if (spawnQueue.Count == 1)
            {
                Debug.Log("starts");
                StartCoroutine(SpawnQueueTimer());
            }
            else if (spawnQueue.Count == 0)
            {
                StopAllCoroutines();
            }
            return ValidObject;
        }

        public void SpawnObject()
        {
            Debug.Log("spawnObject");
            GameObject spawnedObject = Instantiate(spawnQueue[0].order.HumanPrefab, spawnPoint.position, Quaternion.identity);

            Units.Player.PlayerUnit pu = spawnedObject.GetComponent<Units.Player.PlayerUnit>();
            Debug.Log(pu.baseStats.health);
            pu.transform.SetParent(GameObject.Find(pu.unitType.type.ToString()).transform);

            spawnedObject.GetComponent<Units.Player.PlayerUnit>().MoveUnit(flagLocation.position);
            spawnQueue.Remove(spawnQueue[0]);
            ProductionHandler.instance.RemoveFromQueue();
        }

        private Units.BasicUnit IsUnit(string name)
        {
            foreach (Units.BasicUnit unit in ActionFrame.instance.actionList.basicUnits)
            {
                if (unit.name == name)
                {
                    return unit;
                }
            }
            return null;
        }

        private Upgrades.BasicUpgrade IsUpgrade(string name)
        {
            foreach (Upgrades.BasicUpgrade upgrade in ActionFrame.instance.actionList.basicUpgrades)
            {
                if (upgrade.name == name)
                {
                    return upgrade;
                }
            }
            return null;
        }

    }

}
