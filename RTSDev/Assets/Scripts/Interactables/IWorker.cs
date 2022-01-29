using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IWorker : IUnit
    {

        private enum State
        {
            Idle,
            MovingToBuildingZone,
            Building
        }

        private State state;
        private Transform BuildZone;

        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;

        private void Awake()
        {
            state = State.Idle;
        }

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.MovingToBuildingZone:
                    break;
                case State.Building:
                    break;
            }
        }

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
            UI.HUD.ActionFrame.instance.ClearActions();
        }

        public void OnBuilderSelect()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, spawnMarker);
        }
    }
}

