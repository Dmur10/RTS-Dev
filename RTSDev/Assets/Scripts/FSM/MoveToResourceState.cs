using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class MoveToResourceState : AbstractFSMState
    {
        private Interactables.IScavenger scavenger;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.MoveToResource;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                scavenger = unit.GetComponent<Interactables.IScavenger>();
                if (scavenger != null)
                {
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (unit.IsIdle() && scavenger.GetResource()!=null)
            {
                unit.MoveUnit(scavenger.GetResource().position, 10f, () => {
                    fsm.EnterState(FSMStateType.GatherResource);
                });
            }
        }
    }
}
