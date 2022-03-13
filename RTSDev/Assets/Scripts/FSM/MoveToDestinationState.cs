using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    [CreateAssetMenu(fileName = "MoveToDestinationState", menuName = "FSM/States/MoveToDestination", order = 2)]
    public class MoveToDestinationState : AbstractFSMState
    {

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.MoveToDestination;
        }
        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                EnteredState = true;
            }
            return EnteredState;
        }
        public override void UpdateState()
        {
            if (EnteredState)
            {
            }
        }
    }
}
