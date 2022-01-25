using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Buildings
{
    public class BuildingStatDisplay : MonoBehaviour
    {
        public float maxHealth, currentHealth;

        [SerializeField] private Image healthBarAmount;

        private bool isPLayerBuilding = false;

        public void SetStatDisplayBasicBuilding(BuildingStatTypes.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            isPLayerBuilding = isPlayer;

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

