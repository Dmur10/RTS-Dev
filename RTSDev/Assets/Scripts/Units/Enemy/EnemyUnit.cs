using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTSGame.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public GameObject unitStatsDisplay;

        public Image heathBar;

        public float currentHealth;

        private Collider[] colliders;

        private Transform target;

        private bool hasAggro = false;

        private float distance;

        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!hasAggro)
            {
                checkForTarget();
            }
            else
            {
                MoveToTarget();
            }
        }

        private void LateUpdate()
        {
            HandleHeath();
        }

        private void checkForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < colliders.Length; i++)
            { 
                if (colliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer)
                {
                    Debug.Log("hi");
                    target = colliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }

        private void MoveToTarget()
        {
            if (target  == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                distance = Vector3.Distance(target.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange + 1);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(target.position);
                }
            }
            
        }

        private void HandleHeath()
        {
            Camera camera = Camera.main;
            unitStatsDisplay.transform.LookAt(unitStatsDisplay.transform.position +
                camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            heathBar.fillAmount = currentHealth / baseStats.health;
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

