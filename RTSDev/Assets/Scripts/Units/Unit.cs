using RTSGame.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FSM.FiniteStateMachine))]
    public class Unit : MonoBehaviour
    {
        protected NavMeshAgent navAgent;
        private FSM.FiniteStateMachine FiniteStateMachine;

        public BasicUnit unitType;

        public UnitStatTypes.Base baseStats;
        public UnitStatDisplay statDisplay;

        [SerializeField] protected Transform target = null;
        protected StatDisplay targetStatDisplay;

        public Transform waypoint = null;
        private Vector3 destination;

        [SerializeField]
        protected Collider[] colliders;

        private float atkCooldown;
        protected float distance;
        

        public void Start()
        {
            baseStats = unitType.baseStats;
            atkCooldown = baseStats.atkSpeed;
            navAgent = GetComponent<NavMeshAgent>();
            FiniteStateMachine = GetComponent<FSM.FiniteStateMachine>();
        }

        public void Update()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }
        }

        public void SetFiniteState(FSM.FSMStateType type)
        {
            FiniteStateMachine.EnterState(type);
        }

        public FSM.FSMStateType GetFiniteState()
        {
            return FiniteStateMachine.GetCurrentState();
        }

        public bool IsIdle()
        {
            float dist = navAgent.remainingDistance;
            if (!navAgent.pathPending)
            {
                if(navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    if(!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Transform GetWayPoint()
        {
            return waypoint;
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
            if(this.target != null)
            {
                targetStatDisplay = this.target.GetComponentInChildren<StatDisplay>();
                FiniteStateMachine.EnterState(FSM.FSMStateType.Attack);
            }
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
                float distance = Vector3.Distance(target.position, transform.position);
                navAgent.stoppingDistance = (baseStats.atkRange);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(target.position);
                }
        }

        public void AggresiveStance()
        {
            FiniteStateMachine.EnterState(FSM.FSMStateType.Aggressive);
        }

        public void DefensiveStance()
        {
            FiniteStateMachine.EnterState(FSM.FSMStateType.Defensive);
        }

        public void HoldGroundStance()
        {
            FiniteStateMachine.EnterState(FSM.FSMStateType.HoldGround);
        }

        public void Attack()
        {
                targetStatDisplay.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
        }

        public float GetAtkCooldown()
        {
            return atkCooldown;
        }

        public Vector3 GetDestination()
        {
            return destination;
        }

        public void SetDesitnation(Vector3 destination)
        {
            this.destination = destination;
            SetTarget(null);
            FiniteStateMachine.EnterState(FSM.FSMStateType.MoveToDestination);
        }

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

            if (Vector3.Distance(transform.position, destination)  < v)
            {
                navAgent.SetDestination(transform.position);
                p.Invoke();
            }
            else
            {
                navAgent.SetDestination(destination);
            }
        }
    }

}
