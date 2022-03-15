using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
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
                if (unit.GetWayPoint() == null)
                {
                    EnteredState = false;
                } else
                {
                    waypoint = unit.GetWayPoint().GetComponent<EnemyWaypoint>();
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
                } else if(unit.GetWayPoint() == null){
                    fsm.EnterState(FSMStateType.Idle);
                }
                else
                {
                    if (Vector3.Distance(navMeshAgent.transform.position, waypoint.transform.position) < 1f)
                    {
                        unit.SetWaypoint(waypoint.GetNextWaypoint());

                        if (unit.GetWayPoint() == null)
                        {
                            fsm.EnterState(FSMStateType.Idle);
                        }
                        else
                        {
                            SetDestination(waypoint.GetNextWaypoint().position);
                            waypoint = waypoint.GetNextWaypoint().GetComponent<EnemyWaypoint>();
                        }
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

