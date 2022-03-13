using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "FSM/States/Patrol,", order = 2)]
    public class PatrolState : AbstractFSMState
    {
        EnemyWaypoint waypoint;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Patrol;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                waypoint = unit.StartWaypoint.GetComponent<EnemyWaypoint>();
                Debug.Log("entered");

                if(waypoint == null)
                {
                    Debug.Log("null");
                    EnteredState = false;
                } else
                {
                    Debug.Log("not null");
                    SetDestination(waypoint.transform.position);
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (unit.CheckForTarget())
                {
                    fsm.EnterState(FSMStateType.Chase);
                }
                if (waypoint.nextWayPoint == null)
                {
                    fsm.EnterState(FSMStateType.Idle);
                }
                else
                {
                    if (Vector3.Distance(navMeshAgent.transform.position, waypoint.transform.position) < 1f)
                    {
                        SetDestination(waypoint.nextWayPoint.position);
                        waypoint = waypoint.nextWayPoint.GetComponent<EnemyWaypoint>();
                    }
                }
            }
        }

        private void SetDestination(Vector3 destination)
        {
            if(navMeshAgent != null && destination != null)
            {
                navMeshAgent.SetDestination(destination);
            }
        }
    }
}

