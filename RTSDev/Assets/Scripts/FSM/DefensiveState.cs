using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class DefensiveState : AbstractFSMState
    {
        Transform target;
        float distance;
        Vector3 initialPosition;
        float maxDistance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Defensive;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                EnteredState = true;
                initialPosition = unit.transform.position;
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (!unit.HasTarget() && unit.IsIdle())
                {
                    if (unit.CheckForTarget())
                    {
                        target = unit.GetTarget();
                        unit.MoveToTarget();
                    }
                }
                else if( unit.HasTarget())
                {
                    maxDistance = Vector3.Distance(initialPosition, target.position);
                    distance = Vector3.Distance(target.position, unit.transform.position);

                    if ((maxDistance > unit.baseStats.aggroRange + unit.baseStats.atkRange + 1) || (distance > unit.baseStats.aggroRange + 1))
                    {
                        navMeshAgent.SetDestination(initialPosition);
                        unit.SetTarget(null);
                    }
                    else if (unit.GetAtkCooldown() <= 0 && distance < unit.baseStats.atkRange)
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
    }
}
