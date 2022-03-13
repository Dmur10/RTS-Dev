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
            StateType = FSMStateType.Attacking;
        }

        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("enter attacking");

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

