using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class MoveToBuildZoneState : AbstractFSMState
    {

        private Interactables.IWorker worker;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.MoveToBuildZone;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                worker = unit.GetComponent<Interactables.IWorker>();
                if (worker != null)
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
                if (worker.GetBuildZone() != null)
                {
                    Debug.Log(worker.GetBuildZone().GetOffset());
                    unit.MoveUnit(worker.GetBuildZone().GetPosition(), worker.GetBuildZone().GetOffset(), () => {
                        fsm.EnterState(FSMStateType.Building);
                    });
                }
            }
        }
    }
}

