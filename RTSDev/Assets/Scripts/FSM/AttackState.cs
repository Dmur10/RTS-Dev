using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class AttackState : AbstractFSMState
    {
        float distance;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Attack;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                if (unit.HasTarget())
                {
                    EnteredState = true;
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
                    fsm.EnterState(FSMStateType.Idle);
                }
                else
                {
                    unit.MoveToTarget();

                    distance = Vector3.Distance(unit.GetTarget().position, unit.transform.position);
                    if (unit.GetAtkCooldown() <= 0 && distance < unit.baseStats.atkRange)
                    {
                        unit.Attack();
                    }
                    else
                    {
                        navMeshAgent.SetDestination(unit.GetTarget().position);
                    }
                } 
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}

