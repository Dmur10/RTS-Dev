using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class HoldGroundState : AbstractFSMState
    {
        float distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.HoldGround;
        }

        public override void UpdateState()
        {
            if (!unit.HasTarget())
            {
                unit.CheckForTarget();
            }
            else
            {
                distance = Vector3.Distance(unit.GetTarget().position, unit.transform.position);
                if (unit.atkCooldown <= 0 && distance <= unit.baseStats.atkRange)
                {
                    unit.Attack();
                }
                else if (distance > unit.baseStats.aggroRange)
                {
                    unit.SetTarget(null);
                }
            }
        }
    }
}
