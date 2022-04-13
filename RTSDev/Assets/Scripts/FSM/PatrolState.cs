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
            EnteredState = false;
            if (base.EnterState())
            {
                if (unit.GetWayPoint() != null)
                {
                    waypoint = unit.GetWayPoint().GetComponent<EnemyWaypoint>();
                    navMeshAgent.SetDestination(waypoint.transform.position);
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
                    if (Vector3.Distance(unit.transform.position, unit.GetWayPoint().position) < 4f)
                    {
                        if(waypoint.GetNextWaypoint() != null)
                        {
                            unit.SetWaypoint(waypoint.GetNextWaypoint());
                            waypoint = unit.GetWayPoint().GetComponent<EnemyWaypoint>();
                            navMeshAgent.SetDestination(unit.GetWayPoint().position);
                        }
                        else
                        {
                            unit.SetWaypoint(null);
                        }
                    }
                }
            }
        }
    }
}

