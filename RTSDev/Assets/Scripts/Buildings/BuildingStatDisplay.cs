using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Buildings
{
    public class BuildingStatDisplay : StatDisplay
    {
        public void SetStatDisplayBasicBuilding(BuildingStatTypes.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            SetIsPlayer(isPlayer);

            currentHealth = maxHealth;
            setFill();
        }

        protected override void Die()
        {
            if (IsPlayer())
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

