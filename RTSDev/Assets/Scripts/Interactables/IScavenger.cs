using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IScavenger :IUnit
    {
        private enum State
        {
            Idle,
            MovingToResource,
            GatheringResource,
            MovingToStorage
        }

        public RTSResources.ResourceType type;
        private float resourceAmt = 0;
        private float carryLimit = 10;

        private State state;
        private Transform resourceTransform;
        private Transform storageTransform;
        private Units.Player.PlayerUnit unit;

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
                case State.MovingToResource:
                    if(unit.IsIdle())
                    {
                        unit.MoveUnit(resourceTransform.position, 10f, () => {
                           state = State.GatheringResource;
                        });
                    }
                    break;
                case State.GatheringResource:
                    if(unit.IsIdle())
                    {
                        if (resourceAmt > carryLimit - 1 || resourceTransform == null)
                        {
                            storageTransform = Player.PlayerManager.instance.GetClosestStorage(transform.position);
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            PlayAnimationMine(resourceTransform.position, 2f, () =>
                            {
                                resourceAmt++;
                                resourceTransform.gameObject.GetComponent<RTSResources.ResourceSource>().DecrementResource();
                            });
                        }
                    }
                    break;
                case State.MovingToStorage:
                    if (unit.IsIdle())
                    {
                        unit.MoveUnit(storageTransform.position, 10f, () => {
                            Player.PlayerManager.instance.playerResources[(int)type].AddAmount(resourceAmt);
                            resourceAmt = 0;
                            if (resourceTransform != null)
                            {
                                state = State.MovingToResource;
                            }
                            else
                            {
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
        }

        public void SetResource(Transform tf)
        {
            resourceTransform = tf;
            state = State.MovingToResource;
        }

        public void GatherResource(float amount, RTSResources.ResourceType rType)
        {
            if((rType == type) && (resourceAmt+amount < carryLimit))
            {
                resourceAmt += amount;
            }              
        }

        private void PlayAnimationMine(Vector3 position, float v, Action p)
        {
            p.Invoke();
        }
    }
}

