using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class IdleWaveEnemyState : AbstractFSMState
    {

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Idle;
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
            if (unit.CheckForTarget())
            {
                fsm.EnterState(FSMStateType.Chase);
            }
            else if (unit.GetWayPoint() != null)
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