using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class DefensiveState : AbstractFSMState
    {
        private float distance;
        private Vector3 initialPosition;
        private float maxDistance;

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
                        unit.MoveToTarget();
                    }
                }
                else if( unit.HasTarget())
                {
                    maxDistance = Vector3.Distance(initialPosition, unit.GetTarget().position);
                    distance = Vector3.Distance(unit.GetTarget().position, unit.transform.position);

                    if ((maxDistance > unit.baseStats.aggroRange + unit.baseStats.atkRange + 2) || (distance > unit.baseStats.aggroRange + 2))
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
