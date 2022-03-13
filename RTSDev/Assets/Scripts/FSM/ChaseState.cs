using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "FSM/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {
        Transform target;
        StatDisplay targetStatDisplay;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.Chase;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                unit.MoveToTarget();
            }
            return base.EnterState();
        }

        public override void UpdateState()
        {
        }
    }
}

