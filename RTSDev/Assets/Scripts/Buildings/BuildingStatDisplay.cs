using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Buildings
{
    public class BuildingStatDisplay : StatDisplay
    {
        private bool isPLayerBuilding = false;

        public void SetStatDisplayBasicBuilding(BuildingStatTypes.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            isPLayerBuilding = isPlayer;

            currentHealth = maxHealth;
        }

        protected override void Die()
        {
            if (isPLayerBuilding)
            {
                if (InputManager.InputHandler.instance.selectedBuilding == this.transform.parent.gameObject.GetComponent<Player.PlayerBuilding>())
                {
                    InputManager.InputHandler.instance.selectedBuilding = null;
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}

