using System;
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
        private Buildings.BuildingZone BuildZone;
        private Units.Player.PlayerUnit unit;

        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;

        override public void Awake()
        {
            unit = GetComponent<Units.Player.PlayerUnit>();
            state = State.Idle;
        }

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.MovingToBuildingZone:
                    if (BuildZone != null)
                    {
                        unit.MoveUnit(BuildZone.GetPosition(), BuildZone.GetOffset(), () => {
                            state = State.Building;
                        });
                    }
                    break;
                case State.Building:
                    if (unit.IsIdle() && BuildZone != null)
                    {
                        PlayAnimationBuild(BuildZone.GetPosition(), BuildZone.GetOffset(), () =>
                        {
                            BuildZone.AddConstructionTick();
                            if(BuildZone.IsBuilt())
                            {
                                BuildZone = null;
                                state = State.Idle;
                            }
                        });
                    } 
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

        public Buildings.BuildingZone GetBuildZone()
        {
            return BuildZone;
        }

        public void SetBuildZone(Buildings.BuildingZone bz)
        {
            BuildZone = bz;
            state = State.MovingToBuildingZone;
        }

        public void OnBuilderSelect()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, spawnMarker);
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

