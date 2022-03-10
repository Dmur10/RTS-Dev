using System;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units.Player
{
    public class PlayerUnit : Unit
    {

        public enum State
        {
            Idle,
            Moving,
            Attacking
        }

        public State state = State.Idle;

        private bool hasAggro = false;

        private void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
        }

        private void Update()
        {
            atkCooldown -= Time.deltaTime;
            if (hasAggro)
            {
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
                float distance = Vector3.Distance(target.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange + 1);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(target.position);
                }
            }
        }
        

    }
}

