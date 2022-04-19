using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class AggressiveState : AbstractFSMState
    {
        private float distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Aggressive;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                EnteredState = true;
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (!unit.HasTarget())
            {
                if (unit.CheckForTarget())
                {
                    unit.MoveToTarget();
                }
            } else
            {
                distance = Vector3.Distance(unit.GetTarget().position, unit.transform.position);
                if (unit.GetAtkCooldown() <= 0 && distance < unit.baseStats.atkRange)
                {
                    unit.Attack();
                }
                else if (distance > unit.baseStats.aggroRange+2)
                {
                    unit.SetTarget(null);
                }
                else
                {
                    navMeshAgent.SetDestination(unit.GetTarget().position);
                }
            }
        }
    }
}
