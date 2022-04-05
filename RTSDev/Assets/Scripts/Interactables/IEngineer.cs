using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTSGame.Interactables
{
    public class IEngineer : IUnit
    {
        [SerializeField]
        private Transform captureTransform;
        private Units.Player.PlayerUnit unit;

        public int TotalCaptureTicks = 100000;
        private int currentTicks = 0;

        private void Awake()
        {
            unit = GetComponent<Units.Player.PlayerUnit>();
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
