using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class CaptureState : AbstractFSMState
    {
        private Interactables.IEngineer engineer;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Capture;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                engineer = unit.GetComponent<Interactables.IEngineer>();
                EnteredState = true;
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (engineer.GetCapturePoint() != null)
                {
                    Interactables.ICapturable cp = engineer.GetCapturePoint().GetComponent<Interactables.ICapturable>();

                    engineer.PlayAnimationCapture(engineer.GetCapturePoint().position, cp.offset, () =>
                    {
                        cp.AddCaptureTick();
                        if (cp.IsCaptured())
                        {
                            engineer.SetCaptureTarget(null);
                            InputManager.InputHandler.instance.selectedUnits.Remove(engineer.transform);
                            Destroy(engineer.gameObject);
                            fsm.EnterState(FSMStateType.Idle);
                        }
                    });
                    /*
                    if (engineer.ExceededCaptureLimit())
                    {
                        engineer.GetCapturePoint().GetComponent<Interactables.ICapturable>().capture();
                        Destroy(unit.gameObject);
                        fsm.EnterState(FSMStateType.Idle);
                    }
                    else
                    {
                        engineer.PlayAnimationCapture(engineer.GetCapturePoint().position, 2f, () =>
                        {
                            engineer.IncrementTicks();
                        });
                    }
                    */
                }
            }
        }
    }
}

