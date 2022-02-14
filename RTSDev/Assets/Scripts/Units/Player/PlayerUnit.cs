using System;
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
        public BasicUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;
        public UnitStatDisplay statDisplay;

        [SerializeField]private Transform target = null;
        private StatDisplay targetStatDisplay;

        private float distance;
        private bool hasAggro = false;
        public float atkCooldown;

        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
            navAgent = GetComponent<NavMeshAgent>();
            
        }

        private void Update()
        {
            if (hasAggro)
            {
                atkCooldown -= Time.deltaTime;
                Attack();
                MoveToTarget();
            }
        }

        // Update is called once per frame
        public void MoveUnit(Vector3 destination)
        {
            target = null;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(destination);
        }

        public void MoveUnit(Vector3 destination, float v, Action p)
        {
            target = null;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
                navAgent.SetDestination(destination);

            if (Vector3.Distance(transform.position, destination) < v)
            {
                p.Invoke();
            }
        }

        public bool IsIdle()
        {
            if(navAgent.velocity.magnitude <= 0)
            {
                return true;
            }
            return false;
        }

        public void SetTarget(Transform tf)
        {
            target = tf;
            targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
            hasAggro = true;
        }

        private void MoveToTarget()
        {
            if (target == null)
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
            if (distance <= baseStats.atkRange + 1 && atkCooldown <= 0)
            {
                targetStatDisplay.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
            }
        }

    }
}

