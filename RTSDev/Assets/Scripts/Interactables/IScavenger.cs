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
        public float resourceAmt = 0;
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
                    //get resource node
                    resourceTransform = Game.GameHandler.GetResourceNode_Static();
                    state = State.MovingToResource;
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
                        if(resourceAmt > carryLimit-1)
                        {
                            storageTransform = Game.GameHandler.GetStorageNode_Static();
                            state = State.MovingToStorage;
                        }
                        else
                        {
                            PlayAnimationMine(resourceTransform.position, 10f, () => {
                                resourceAmt++;
                            })
                        }
                    }
                    break;
                case State.MovingToStorage:
                    if (unit.IsIdle())
                    {
                        unit.MoveUnit(storageTransform.position, 10f, () => {
                            Player.PlayerManager.instance.playerResources[(int)type].AddAmount(resourceAmt);
                            resourceAmt = 0;
                            state = State.Idle;
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
        }

        public void SetStorage(Transform tf)
        {
            storageTransform = tf;
        }

        public void GatherResource(float amount, RTSResources.ResourceType rType)
        {
            if((rType == type) && (resourceAmt+amount < carryLimit))
            {
                resourceAmt += amount;
            }              
        }
        public void DumpResource()
        {
            storageTransform.gameObject.GetComponent<IStorage>().StoreResource(resourceAmt, type);
            resourceAmt = 0;
        }

        private void PlayAnimationMine(Vector3 position, float v, Action p)
        {
            throw new NotImplementedException();
        }
    }
}

