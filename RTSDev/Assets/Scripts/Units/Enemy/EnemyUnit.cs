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

        public enum State
        {
            Idle,
            Moving,
            Attacking
        }

        public State state = State.Idle;

        private NavMeshAgent navAgent;
        public Transform currentWaypoint;

        public BasicUnit unitType;
        [HideInInspector]
        public UnitStatTypes.Base baseStats;
        public UnitStatDisplay statDisplay;

        private Collider[] colliders;
        [SerializeField]private Transform target;
        private UnitStatDisplay targetUnit;

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

            switch (state)
            {
                case State.Idle:
                    checkForTarget();
                    break;
                case State.Moving:
                    if(currentWaypoint == null)
                    {
                        state = State.Idle;
                    }
                    checkForTarget();
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
                    targetUnit = target.gameObject.GetComponentInChildren<UnitStatDisplay>();
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

