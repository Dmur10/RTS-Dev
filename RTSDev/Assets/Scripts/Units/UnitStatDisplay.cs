using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Units
{
    public class UnitStatDisplay : StatDisplay
    {
        public void SetStatDisplayBasicUnit(UnitStatTypes.Base stats, bool isPlayer )
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

