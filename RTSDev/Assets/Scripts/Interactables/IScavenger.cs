using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{

    public enum ScavengerStates
    {
        Idle,
        Gather,
        Store
    }
    public class IScavenger :IUnit
    {
        public RTSResources.ResourceType type;
        public float resourceAmt = 0;
        private float carryAmt = 10;
        public ScavengerStates state = ScavengerStates.Idle;
        public Transform resource;
        public Transform storage;

        public void Update()
        {
            switch(state)
            {
                case ScavengerStates.Idle:
                    break;
                //case gather:
                    //if target null find resource
                    //go to resource and gather more
                //case store:
                    //if storage null find dropOff point
                    //move to dropoff point dump resource
                    //give player resource
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
            resource = tf;
        }

        public void SetStorage(Transform tf)
        {
            storage = tf;
        }

        public void GatherResource(float amount, RTSResources.ResourceType rType)
        {
            if((rType == type) && (resourceAmt+amount < carryAmt))
            {
                resourceAmt += amount;
            }              
        }
        public void DumpResource()
        {
            storage.gameObject.GetComponent<IStorage>().StoreResource(resourceAmt, type);
            resourceAmt = 0;
        }
    }
}

