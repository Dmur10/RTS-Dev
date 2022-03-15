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

        public Transform waypoint;

        [SerializeField]
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

        void Update()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }
        }

        public bool IsIdle()
        {
            if(navAgent.velocity.magnitude != 0)
            {
                return false;
            }
            return true;
        }

        public void SetWaypoint(Transform waypoint)
        {
            this.waypoint = waypoint;
        }

        public Transform GetTarget()
        {
            return target;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public bool HasTarget()
        {
            if(target != null)
            {
                return true;
            }
            return false;
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

        public void Attack()
        {
                targetStatDisplay.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
        }
    }

}
