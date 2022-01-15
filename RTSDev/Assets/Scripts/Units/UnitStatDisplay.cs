using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {

        public float maxHealth, currentHealth;

        [SerializeField] private Image healthBarAmount;

        private bool isPLayerUnit = false;

        // Start is called before the first frame update
        private void Start()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                isPLayerUnit = true;
            }
            catch (Exception)
            {
                Debug.Log("No player Unit");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;
                    isPLayerUnit = false;
                }
                catch(Exception)
                {
                    Debug.Log("No Scripts Found");
                } 
            }
            currentHealth = maxHealth;
        }

        // Update is called once per frame
         private void Update()
         {
            HandleHeath();
         }

        public void takeDamage(float damage)
        {
            float totalDamage = damage;
            currentHealth -= totalDamage;
        }

        private void HandleHeath()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position +
                camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;
            if (currentHealth <= 0)
            { 
                Die();
            }
        }

        private void Die()
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

