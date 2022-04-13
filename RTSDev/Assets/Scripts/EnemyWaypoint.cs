using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public class EnemyWaypoint : MonoBehaviour
    {
        [SerializeField]
        private Transform[] nextWayPoints;
        [SerializeField]
        private bool lastWaypoint = false;

        public bool isLastWayPoint()
        {
            return lastWaypoint;
        }

        public Transform GetNextWaypoint()
        {
            if (lastWaypoint)
            {
                return null;
            }
            return nextWayPoints[Random.Range(0,nextWayPoints.Length)];
        }
    }
}
