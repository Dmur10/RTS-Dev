using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RTSGame.Units
{
    public class Health : MonoBehaviour
    {

        public GameObject unitStatsDisplay;

        public Image heathBar;

        public float currentHealth;

        // Update is called once per frame
        private void Update()
        {
            if (gameObject.GetComponent<Player.PlayerUnit>())
            {

            } else if(gameObject.GetComponent<Enemy.EnemyUnit>() ){

            }
        }

        private void HandleHeath()
        {
            Camera camera = Camera.main;
            unitStatsDisplay.transform.LookAt(unitStatsDisplay.transform.position +
                camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

           // heathBar.fillAmount = currentHealth / baseStats.health;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}



