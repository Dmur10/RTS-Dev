using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="FSM/States/Idle,",order =1)]
    public class IdleState : AbstractFSMState
    {

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Idle;
        }
        public override bool EnterState()
        {
            base.EnterState();

            EnteredState = true;
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (unit.CheckForTarget())
            {
                fsm.EnterState(FSMStateType.Chase);
            } else if(unit.StartWaypoint != null)
            {
                fsm.EnterState(FSMStateType.Patrol);
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}


