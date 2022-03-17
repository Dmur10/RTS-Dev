using System;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units.Player
{
    public class PlayerUnit : Unit
    {

        private void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
        }

        // Update is called once per frame

        public override bool CheckForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < colliders.Length;)
            {
                target = colliders[i].gameObject.transform;
                targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
                return true;
            }
            return false;
        }
        

    }
}

