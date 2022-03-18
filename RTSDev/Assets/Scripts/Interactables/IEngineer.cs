using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTSGame.Interactables
{
    public class IEngineer : IUnit
    {
        /*private enum State
        {
            Idle,
            MovingToCapturePoint,
            Capturing
        }*/

        //private State state;
        private Transform captureTransform;
        private Units.Player.PlayerUnit unit;

        public int TotalCaptureTicks;
        private int currentTicks = 0;

        private void Awake()
        {
            unit = GetComponent<Units.Player.PlayerUnit>();
            //state = State.Idle;
        }
        private void Update()
        {
           /* switch (state)
            {
                case State.Idle:
                    break;
                case State.MovingToCapturePoint:
                    if (unit.IsIdle())
                    {
                        unit.MoveUnit(captureTransform.position, 10f, () => {
                            state = State.Capturing;
                        });
                    }
                    break;
                case State.Capturing:
                    if (unit.IsIdle())
                    {
                        if ( currentTicks <= TotalCaptureTicks || captureTransform == null)
                        {
                            captureTransform.GetComponent<ICapturable>().capture();
                        }
                        else
                        {
                            PlayAnimationCapture(captureTransform.position, 2f, () =>
                            {
                                currentTicks++;
                            });
                        }
                    }
                    break;
            }*/
        }
        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }

        public Transform GetCapturePoint()
        {
            return captureTransform;
        }

        public void SetCaptureTarget(Transform tf)
        {
            captureTransform = tf;
            unit.SetFiniteState(FSM.FSMStateType.MoveToCapturePoint);
            //state = State.MovingToCapturePoint;
        }

        public bool ExceededCaptureLimit()
        {
            if(currentTicks >= TotalCaptureTicks)
            {
                currentTicks = 0;
                return true;
            }
            return false;
        }

        public void IncrementTicks()
        {
            currentTicks++;
        }

        public void PlayAnimationCapture(Vector3 position, float v, Action p)
        {
            p.Invoke();
        }
    }
}
