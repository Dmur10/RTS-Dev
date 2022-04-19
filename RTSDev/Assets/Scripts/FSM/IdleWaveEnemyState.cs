using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class IdleWaveEnemyState : AbstractFSMState
    {
        private float MaxWaitTime = 10f;
        private float waitTime = 0f;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Idle;
        }
        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                navMeshAgent.stoppingDistance = 0;
                waitTime = MaxWaitTime;
                EnteredState = true;
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (unit.CheckForTarget())
            {
                fsm.EnterState(FSMStateType.Chase);
            }
            else if (unit.GetWayPoint() != null)
            {
                fsm.EnterState(FSMStateType.Patrol);
            }

            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Transform target = Player.PlayerManager.instance.GetClosestPlayerObject(unit.transform.position);
                unit.SetTarget(target);
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}