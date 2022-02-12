using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Units
{
    public class UnitStatDisplay : StatDisplay
    {
        private bool isPLayerUnit = false;

        public void SetStatDisplayBasicUnit(UnitStatTypes.Base stats, bool isPlayer )
        {
            maxHealth = stats.health;
            isPLayerUnit = isPlayer;

            currentHealth = maxHealth;
        }

        protected override void Die()
        {
            if (isPLayerUnit)
            {
                InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform.parent);
                Destroy(gameObject.transform.parent.gameObject);
            } 
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}

