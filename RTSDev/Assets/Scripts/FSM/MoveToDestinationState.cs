using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class MoveToDestinationState : AbstractFSMState
    {

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.MoveToDestination;
        }
        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                unit.SetTarget(null);
                navMeshAgent.stoppingDistance = 0;
                unit.MoveUnit(unit.GetDestination());
                EnteredState = true;
            }
            return EnteredState;
        }
        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (unit.IsIdle())
                {
                    fsm.EnterState(FSMStateType.Idle);
                }
            }
        }
    }
}
