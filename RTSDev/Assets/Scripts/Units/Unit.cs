using RTSGame.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units
{
    public enum State
    {
        Idle,
        Moving,
        Attacking,
        Aggresive,
        Defensive,
        StandGround
    }

    [RequireComponent(typeof(NavMeshAgent), typeof(FSM.FiniteStateMachine))]
    public class Unit : MonoBehaviour
    {
        protected NavMeshAgent navAgent;
        protected FSM.FiniteStateMachine finiteStateMachine;

        public BasicUnit unitType;

        public UnitStatTypes.Base baseStats;
        public UnitStatDisplay statDisplay;

        [SerializeField] protected Transform target = null;
        protected StatDisplay targetStatDisplay;

        public Transform StartWaypoint;

        protected Collider[] colliders;

        public float atkCooldown;
        protected float distance;
        

        public void Start()
        {
            baseStats = unitType.baseStats;
            atkCooldown = baseStats.atkSpeed;
            navAgent = GetComponent<NavMeshAgent>();
            finiteStateMachine = GetComponent<FSM.FiniteStateMachine>();
        }

        public virtual bool CheckForTarget()
        {
            return false;
        }
        public void MoveToTarget()
        {
            if (target == null)
            {
                navAgent.SetDestination(transform.position);
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

        protected void Attack()
        {
            if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
            {
                targetStatDisplay.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
            }
        }
    }

}
