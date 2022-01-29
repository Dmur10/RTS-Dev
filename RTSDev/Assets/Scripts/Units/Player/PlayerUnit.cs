using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RTSGame.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {

        private NavMeshAgent navAgent;
        public BasicUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
            navAgent = GetComponent<NavMeshAgent>();
            
        }  

        // Update is called once per frame
        public void MoveUnit(Vector3 destination)
        {
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(destination);
        }

        public void MoveUnit(Vector3 destination, float v, Action p)
        {
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
                navAgent.SetDestination(destination);
            if (Vector3.Distance(transform.position, destination) < v)
            {
                p.Invoke();
            }
        }

        public bool IsIdle()
        {
            if(navAgent.velocity.magnitude <= 0)
            {
                return true;
            }
            return false;
        }
    }
}

