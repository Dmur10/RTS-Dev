using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class ChaseState : AbstractFSMState
    {
        private float distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Chase;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                if(unit.GetTarget() != null)
                {
                    EnteredState = true;
                    unit.MoveToTarget();
                }
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (unit.GetTarget() == null)
                {
                    navMeshAgent.stoppingDistance = 0;
                    navMeshAgent.SetDestination(unit.transform.position);
                    if (unit.GetWayPoint() != null)
                    {
                        fsm.EnterState(FSMStateType.Patrol);
                    }
                    else
                    {
                        fsm.EnterState(FSMStateType.Idle);
                    }
                }
                else
                {
                    distance = Vector3.Distance(unit.GetTarget().position, unit.transform.position);

                    if (unit.GetAtkCooldown() <= 0 && distance <= unit.baseStats.atkRange)
                    {
                        Debug.Log("Attacks");
                        unit.Attack();
                    }
                    else if (distance > unit.baseStats.aggroRange+2)
                    {
                        navMeshAgent.SetDestination(unit.transform.position);
                        unit.SetTarget(null);
                        fsm.EnterState(FSMStateType.Idle);
                    }
                    else
                    {
                        navMeshAgent.SetDestination(unit.GetTarget().position);
                    }
                }
            }
            
        }
    }
}

