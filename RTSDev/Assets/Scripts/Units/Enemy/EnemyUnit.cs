using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTSGame.Units.Enemy
{
    public class EnemyUnit : Unit
    {
        private void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, false);
        }

        private void Update()
        {
            atkCooldown -= Time.deltaTime;

        /*    switch (state)
            {
                case State.Idle:
                    checkForTarget();
                    if(StartWaypoint != null)
                    {
                        MoveUnit(StartWaypoint.position);
                    }
                    break;
                case State.Moving:
                    checkForTarget();
                    if (StartWaypoint == null)
                    {
                        state = State.Idle;
                    }
                    break;
                case State.Attacking:
                    if(target == null)
                    {
                        state = State.Moving;
                    }
                    Attack();
                    MoveToTarget();
                    break;
            }*/
        }

        public override bool CheckForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

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

