using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        NavMeshAgent navAgent;

        public UnitStatTypes.Base baseStats;

        public Collider[] colliders;

        public Transform target;

        public bool hasAggro = false;

        public float distance;

        private void Start()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!hasAggro)
            {
                checkForTarget();
            }
            else
            {
                MoveToTarget();
            }
        }

        private void checkForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            for (int i = 0; i < colliders.Length; i++)
            { 
                if (colliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer)
                {
                    Debug.Log("hi");
                    target = colliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }

        private void MoveToTarget()
        {
            distance = Vector3.Distance(target.position, transform.position);
            navAgent.stoppingDistance = (baseStats.atkRange + 1);

            if (distance <= baseStats.aggroRange)
            {
                navAgent.SetDestination(target.position);
            }
        }
    }
}

