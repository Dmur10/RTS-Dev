using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class IdleUtilityUnitState : AbstractFSMState
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
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}

