using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class MoveToCapturePointState : AbstractFSMState
    {
        private Interactables.IEngineer engineer;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.MoveToCapturePoint;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                engineer = unit.GetComponent<Interactables.IEngineer>();
                if(engineer != null)
                {
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (unit.IsIdle())
                {
                    unit.MoveUnit(engineer.GetCapturePoint().position, 10f, () => {
                        fsm.EnterState(FSMStateType.Capture);
                    });
                }
            }
            
        }
    }
}

