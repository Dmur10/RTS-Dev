using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace RTSGame.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {

        private NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public GameObject unitStatsDisplay;

        public Image heathBar;

        public float currentHealth;
        
        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }

        private void handleHeath()
        {
            Camera camera  
        }
    }
}

