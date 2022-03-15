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
        public void MoveUnit(Vector3 destination)
        {
            target = null;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(destination);
        }

        public void MoveUnit(Vector3 destination, float v, Action p)
        {
            target = null;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }

            if (Vector3.Distance(transform.position, destination) < v)
            {
                navAgent.SetDestination(transform.position);
                p.Invoke();
            }
            else
            {
                navAgent.SetDestination(destination);
            }
        }

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

