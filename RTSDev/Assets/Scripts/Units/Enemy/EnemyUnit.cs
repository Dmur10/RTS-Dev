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

        private Player.PlayerUnit targetUnit;

        private bool hasAggro = false;

        private float distance;

        private float atkCooldown;

        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            currentHealth = baseStats.health;
        }

        private void Update()
        {
            atkCooldown -= Time.deltaTime;
            if (!hasAggro)
            {
                checkForTarget();
            }
            else
            {
                Attack();
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
                    target = colliders[i].gameObject.transform;
                    targetUnit = target.gameObject.GetComponent<Player.PlayerUnit>();
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

        private void Attack()
        {
            if(atkCooldown <= 0 && distance <= baseStats.atkRange+1)
            {
                targetUnit.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
            }
        }

        public void takeDamage(float damage)
        {
            float totalDamage = damage;
            currentHealth -= totalDamage;
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

