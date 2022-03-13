using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "FSM/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {
        Transform target;
        StatDisplay targetStatDisplay;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Chase;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                target = unit.GetTarget();
                if(target != null)
                {
                    unit.MoveToTarget();
                }
            }
            return base.EnterState();
        }

        public override void UpdateState()
        {
            if(target = null)
            {
                navMeshAgent.SetDestination(unit.transform.position);
                fsm.EnterState(FSMStateType.Patrol);
            }
        }

        private void MoveToTarget()
        {
                float distance = Vector3.Distance(target.position, unit.transform.position);
                navMeshAgent.stoppingDistance = (unit.baseStats.atkRange + 1);

                if (distance <= unit.baseStats.aggroRange)
                {
                    navMeshAgent.SetDestination(target.position);
                }
        }
    }
}

