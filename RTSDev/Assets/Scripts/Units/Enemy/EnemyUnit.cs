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
        private NavMeshAgent navAgent;

        public BasicUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        private Collider[] colliders;

        private Transform target;

        private UnitStatDisplay targetUnit;

        private bool hasAggro = false;

        private float distance;

        public float atkCooldown;

        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, false);
            navAgent = gameObject.GetComponent<NavMeshAgent>(); 
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
         
        private void checkForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < colliders.Length;)
            { 
                    target = colliders[i].gameObject.transform;
                    targetUnit = target.gameObject.GetComponentInChildren<UnitStatDisplay>();
                    hasAggro = true;
                    break;
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

    }
}

