using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{

    public struct SpawnableObject
    {
        public float queue;
        public GameObject order;
        public GameObject spawnPoint;
        public Transform spawnLocation;

        public SpawnableObject(float queue, GameObject order, GameObject spawnPoint, Transform spawnLocation)
        {
            this.queue = queue;
            this.order = order;
            this.spawnPoint = spawnPoint;
            this.spawnLocation = spawnLocation;
        }
    }

    public class ActionFrame : MonoBehaviour
    {
        
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButtonUnit = null;
        [SerializeField] private Button actionButtonBuilding = null;
        [SerializeField] private Button actionButtonUpgrade = null;

        [SerializeField] private Transform layoutGroup = null;
        [SerializeField] private GameObject toolTip;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionList = null;

        public List<SpawnableObject> spawnQueue = new List<SpawnableObject>();

        public GameObject spawnPoint = null;
        public Transform spawnLocation = null;

        private void Awake()
        {
                instance = this;
        
        }

        public void SetActionButtons(PlayerActions actions, GameObject spawnPoint = null, Transform spawnLocation = null)
        {
            actionList = actions;
            if (spawnPoint != null && spawnLocation != null)
            {
                this.spawnPoint = spawnPoint;
                this.spawnLocation = spawnLocation;
            }

            if (actions.basicUnits.Count>0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButtonUnit, layoutGroup);
                    btn.name = unit.name;
                    buttons.Add(btn);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButtonBuilding, layoutGroup);
                    btn.name = building.name;
                    buttons.Add(btn);
                }
            }

            if(actions.basicUpgrades.Count > 0)
            {
                foreach(Upgrades.BasicUpgrade upgrade in actions.basicUpgrades)
                {
                    Button btn = Instantiate(actionButtonUpgrade, layoutGroup);
                    btn.name = upgrade.name;
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach(Button btn in buttons)
            { 
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

       public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn) && spawnQueue.Count < 9)//spawnOrder.Count < 9)
            {
                Units.BasicUnit unit = IsUnit(objectToSpawn);
                ProductionQueue.instance.AddToQueue();
                spawnQueue.Add(new SpawnableObject(unit.SpawnTime, unit.HumanPrefab, spawnPoint, spawnLocation));
            }
            else
            {
                Debug.Log($"{objectToSpawn} is not spawnable");
            }

            if (spawnQueue.Count == 1) 
            {
                Debug.Log("starts");
                ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueueTimer());
            } else if (spawnQueue.Count == 0)
            {
                ActionTimer.instance.StopAllCoroutines();
            }
            
        }

        public void Upgrade(string name)
        {
            Upgrades.BasicUpgrade upgrade = IsUpgrade(name);
            if(upgrade != null)
            {
                Upgrades.UpgradeHandler.instance.ApplyUpgrade(upgrade);
            }
        }

        private Units.BasicUnit IsUnit(string name)
        {
            foreach (Units.BasicUnit unit in actionList.basicUnits)
            {
                if (unit.name == name)
                {
                    return unit;
                }
            }
            return null;
        }

        private Buildings.BasicBuilding IsBuilding(string name)
        {
            foreach (Buildings.BasicBuilding building in actionList.basicBuildings)
            {
                if (building.name == name)
                {
                    return building;
                }
            }
            return null;
        }

        private Upgrades.BasicUpgrade IsUpgrade(string name)
        {
            foreach(Upgrades.BasicUpgrade upgrade in actionList.basicUpgrades)
            {
                if(upgrade.name == name)
                {
                    return upgrade;
                }
            }
            Debug.Log("fail");
            return null;
        }

        public void SpawnObject()
        {
            ProductionQueue.instance.SetProgressAmount(0);
            Debug.Log("spawnObject");
            GameObject spawnedObject = Instantiate(spawnQueue[0].order, spawnQueue[0].spawnLocation.position, Quaternion.identity);

            Units.Player.PlayerUnit pu = spawnedObject.GetComponent<Units.Player.PlayerUnit>();
            Debug.Log(pu.baseStats.health);
            pu.transform.SetParent(GameObject.Find(pu.unitType.type.ToString()).transform);

            spawnedObject.GetComponent<Units.Player.PlayerUnit>().MoveUnit(spawnQueue[0].spawnPoint.transform.position);
            spawnQueue.Remove(spawnQueue[0]);
            ProductionQueue.instance.RemoveFromQueue();
        }
    }

}
