using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IScavenger :IUnit
    {
        public RTSResources.ResourceType type;
        public float resourceAmt = 0;
        private float carryAmt = 10;

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }

        public void GatherResource(float amount, RTSResources.ResourceType rType)
        {
            if((rType == type) && (resourceAmt+amount < carryAmt))
            {
                resourceAmt += amount;
            }              
        }
        public float DumpResource()
        {
            float temp = resourceAmt;
            resourceAmt = 0;
            return temp;
        }
    }
}

