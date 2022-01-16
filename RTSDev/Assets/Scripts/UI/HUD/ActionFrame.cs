using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionList = null;

        public List<float> spawnTimers = new List<float>();
        public List<GameObject> spawnOrder = new List<GameObject>();

        private void Awake()
        {
                instance = this;
        
        }

        public void SetActionButtons(PlayerActions actions)
        {
            actionList = actions;
            if (actions.basicUnits.Count>0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;

                    buttons.Add(btn);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;

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

       public void Spawn(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {
                Units.BasicUnit unit = IsUnit(objectToSpawn);
                spawnTimers.Add(unit.SpawnTime);
                spawnOrder.Add(unit.HumanPrefab);
            }
            else
            {
                Debug.Log($"{objectToSpawn} is not spawnable");
            }
            ActionTimer.instance.SpawnQueueTimer();
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

    }

   
}
