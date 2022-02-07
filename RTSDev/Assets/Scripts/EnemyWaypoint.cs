using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public class EnemyWaypoint : MonoBehaviour
    {
        public Transform nextWayPoint;
        public bool isLastWaypoint;

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                if (isLastWaypoint)
                {
                    other.GetComponent<Units.Enemy.EnemyUnit>().currentWaypoint = null;
                }
                else
                {
                    other.GetComponent<Units.Enemy.EnemyUnit>().currentWaypoint = nextWayPoint;
                }  
            }
        }
    }
}
