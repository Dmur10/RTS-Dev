using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButtonUnit = null;
        [SerializeField] private Button actionButtonBuilding = null;
        [SerializeField] private Button actionButtonUpgrade = null;

        [SerializeField] private Transform layoutGroup = null;
        [SerializeField] private GameObject toolTip;

        private List<Button> buttons = new List<Button>();
        public PlayerActions actionList = null;

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
                    Button btn = Instantiate(actionButtonUnit, layoutGroup);
                    btn.name = unit.name;
                    btn.GetComponent<BuildUnit>().basicUnit = unit;
                    buttons.Add(btn);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButtonBuilding, layoutGroup);
                    btn.name = building.name;
                    btn.GetComponent<BuildBuilding>().basicBuilding = building;
                    buttons.Add(btn);
                }
            }

            if(actions.basicUpgrades.Count > 0)
            {
                foreach(Upgrades.BasicUpgrade upgrade in actions.basicUpgrades)
                {
                    Button btn = Instantiate(actionButtonUpgrade, layoutGroup);
                    btn.name = upgrade.name;
                    btn.GetComponent<PurchaseUpgrade>().basicUpgrade = upgrade;
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
    }

}
