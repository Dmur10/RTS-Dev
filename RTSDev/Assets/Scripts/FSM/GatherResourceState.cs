using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.FSM
{
    public class GatherResourceState : AbstractFSMState
    {
        private Interactables.IScavenger scavenger;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.GatherResource;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                scavenger = unit.GetComponent<Interactables.IScavenger>();
                if (scavenger != null)
                {
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (unit.IsIdle())
            {
                if (scavenger.ExceededLimit() || scavenger.GetResource() == null)
                {
                    scavenger.SetStorage(Player.PlayerManager.instance.GetClosestStorage(transform.position));
                    fsm.EnterState(FSMStateType.MoveToStorage);
                }
                else
                {
                    scavenger.PlayAnimationMine(scavenger.GetResource().position, 2f, () =>
                    {
                        scavenger.SetResourceAmount(scavenger.GetResourceAmount() + 1);
                        scavenger.GetResource().gameObject.GetComponent<RTSResources.ResourceSource>().DecrementResource();
                    });
                }
            }
        }
    }
}
