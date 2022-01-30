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

        private void Awake()
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
                    if (unit.IsIdle())
                    {
                        unit.MoveUnit(BuildZone.GetPosition(), 10f, () => {
                            state = State.Building;
                        });
                    }
                    break;
                case State.Building:
                    if (unit.IsIdle())
                    {
                        PlayAnimationBuild(BuildZone.GetPosition(), 20f, () =>
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

        public void SetBuildZone(Buildings.BuildingZone bz)
        {
            BuildZone = bz;
            state = State.MovingToBuildingZone;
        }

        public void OnBuilderSelect()
        {
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, spawnMarker);
        }

        private void PlayAnimationBuild(Vector3 position, float v, Action p)
        {
            p.Invoke();
        }
    }
}

