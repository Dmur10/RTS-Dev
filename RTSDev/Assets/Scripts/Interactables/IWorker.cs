using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IWorker : IUnit
    {

        private Buildings.BuildingZone BuildZone;
        private Units.Player.PlayerUnit unit;

        public UI.HUD.PlayerActions actions;

        override public void Awake()
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
            UI.HUD.ActionFrame.instance.ClearActions();
        }

        public Buildings.BuildingZone GetBuildZone()
        {
            return BuildZone;
        }

        public void SetBuildZone(Buildings.BuildingZone bz)
        {
            BuildZone = bz;
            unit.SetFiniteState(FSM.FSMStateType.MoveToBuildZone);
        }

        public void OnBuilderSelect()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            UI.HUD.ActionFrame.instance.SetActionButtons(actions);
        }

        public void PlayAnimationBuild(Vector3 position, float v, Action p)
        {
            if (Vector3.Distance(transform.position, position) < v)
            {
                p.Invoke();
            }
        }
    }
}

