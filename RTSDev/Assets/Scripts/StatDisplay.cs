using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame
{
    public class StatDisplay : MonoBehaviour
    {
        public float maxHealth, currentHealth;

        [SerializeField] private Image healthBarAmount;

        private bool isPlayer;

        // Update is called once per frame
        private void Update()
        {
            HandleHeath();
        }

        public void takeDamage(float damage)
        {
            float totalDamage = damage;
            currentHealth -= totalDamage;
            setFill();
        }

        public void SetIsPlayer(bool player)
        {
            this.isPlayer = player;
        }

        public bool IsPlayer()
        {
            return isPlayer;
        }

        public void setFill()
        {
            healthBarAmount.fillAmount = currentHealth / maxHealth;
        }

        private void HandleHeath()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position +
                camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {

        }
    }

}
