using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Moveable : MonoBehaviour
    {

        private NavMeshAgent navAgent;
        
        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }
    }
}

