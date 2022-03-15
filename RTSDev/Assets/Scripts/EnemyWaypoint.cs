using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public class EnemyWaypoint : MonoBehaviour
    {
        public Transform nextWayPoint = null;
        [SerializeField]
        bool lastWaypoint = false;

        public bool isLastWayPoint()
        {
            return lastWaypoint;
        }

        public Transform GetNextWaypoint()
        {
            return nextWayPoint;
        }
    }
}
