using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTSGame.Units.Enemy
{
    public class EnemyUnit : Unit
    {
        public enum State
        {
            Idle,
            Moving,
            Attacking
        }

        public State state = State.Idle;

        public Transform currentWaypoint;

        private Collider[] colliders;

        private void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, false);
        }

        private void Update()
        {
            atkCooldown -= Time.deltaTime;

            switch (state)
            {
                case State.Idle:
                    checkForTarget();
                    if(currentWaypoint != null)
                    {
                        MoveUnit(currentWaypoint.position);
                    }
                    break;
                case State.Moving:
                    checkForTarget();
                    if (currentWaypoint == null)
                    {
                        state = State.Idle;
                    }
                    break;
                case State.Attacking:
                    if(target == null)
                    {
                        state = State.Moving;
                    }
                    Attack();
                    MoveToTarget();
                    break;
            }
        }
        public void MoveUnit(Vector3 destination)
        {
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(destination);
            state = State.Moving;
        }

        private void checkForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < colliders.Length;)
            { 
                    target = colliders[i].gameObject.transform;
                    targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
                    state = State.Attacking;
                    break;
            }
        }

        private void MoveToTarget()
        {
            if (target  == null)
            {
                navAgent.SetDestination(transform.position);
                state = State.Moving;
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

