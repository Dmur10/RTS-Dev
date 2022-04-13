using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTSGame.Units.Enemy
{
    public class EnemyUnit : Unit
    {
        public void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, false);
        }

        public override bool CheckForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log(colliders[i].tag);
                if (colliders[i].gameObject.CompareTag("Player"))
                {
                    target = colliders[i].gameObject.transform;
                    targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
                    return true;
                }
            }
            target = null;
            return false;
        }       

    }
}

