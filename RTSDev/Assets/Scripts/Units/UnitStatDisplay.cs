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
                maxHealth = gameObject.GetComponent.InParent<Player.PlayerUnit>().baseStats.health;
                isPLayerUnit = true;
            }
            catch (Exception)
            {
                Debug.Log("No player Unit");
                try
                {
                    maxHealth = gameObject.GetComponent.InParent<Enemy.EnemyUnit>().baseStats.health;
                    isPLayerUnit = false;
                }
                catch
                {
                    Debug.Log("No Scripts Found");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

