using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class AttackingState : AbstractFSMState
    {
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
                EnteredState = true;
            }

            EnteredState = true;
            return EnteredState;
        }

        public override void UpdateState()
        {
            //Move to target and attack if in range
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}

