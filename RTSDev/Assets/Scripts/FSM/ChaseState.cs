using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class ChaseState : AbstractFSMState
    {
        Transform target;
        float distance;

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
                target = unit.GetTarget();
                if(target != null)
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
                if (target == null)
                {
                    navMeshAgent.SetDestination(unit.transform.position);
                    fsm.EnterState(FSMStateType.Patrol);
                }
                else
                {
                    distance = Vector3.Distance(target.position, unit.transform.position);
                    if (unit.atkCooldown > 0)
                    {
                        unit.atkCooldown -= Time.deltaTime;
                    }

                    if (unit.atkCooldown <= 0 && distance <= unit.baseStats.atkRange)
                    {
                        unit.Attack();
                    }
                    else if (distance > unit.baseStats.aggroRange)
                    {
                        navMeshAgent.SetDestination(unit.transform.position);
                        unit.SetTarget(null);
                        fsm.EnterState(FSMStateType.Idle);
                    }
                    else
                    {
                        navMeshAgent.SetDestination(target.position);
                    }
                }
            }
            
        }

        private void MoveToTarget()
        {
                distance = Vector3.Distance(target.position, unit.transform.position);
                navMeshAgent.stoppingDistance = (unit.baseStats.atkRange + 1);

                if (distance <= unit.baseStats.aggroRange)
                {
                    navMeshAgent.SetDestination(target.position);
                }
        }
    }
}

