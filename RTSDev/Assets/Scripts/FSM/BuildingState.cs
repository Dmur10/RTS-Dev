using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class BuildingState : AbstractFSMState
    {
        private Interactables.IWorker worker;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Building;
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
                if (unit.IsIdle() && worker.GetBuildZone() != null)
                {
                    Buildings.BuildingZone bz = worker.GetBuildZone();
                    worker.PlayAnimationBuild(bz.GetPosition(), bz.GetOffset(), () =>
                    {
                        bz.AddConstructionTick();
                        if (bz.IsBuilt())
                        {
                            worker.SetBuildZone(null);
                            fsm.EnterState(FSMStateType.Idle);
                        }
                    });
                }
            }
        }
    }
}
