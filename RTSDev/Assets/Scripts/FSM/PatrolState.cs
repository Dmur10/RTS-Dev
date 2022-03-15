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
                waypoint = unit.waypoint.GetComponent<EnemyWaypoint>();

                if(waypoint == null)
                {
                    EnteredState = false;
                } else
                {
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
                else
                {
                    if (Vector3.Distance(navMeshAgent.transform.position, waypoint.transform.position) < 1f)
                    {
                        unit.SetWaypoint(waypoint.GetNextWaypoint());

                        if (unit.waypoint == null)
                        {
                            fsm.EnterState(FSMStateType.Idle);
                        }
                        else
                        {
                            SetDestination(waypoint.GetNextWaypoint().position);
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

