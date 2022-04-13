using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.UI.HUD
{
    public class CommandBox : MonoBehaviour
    {
        public void SetIdleStance()
        {
            foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                if(unit.GetComponent<Units.Unit>().GetFiniteState() != FSM.FSMStateType.Idle)
                {
                    unit.GetComponent<Units.Unit>().SetFiniteState(FSM.FSMStateType.Idle);
                }
            }
        }

        public void SetAggressiveState()
        {
            if (AllAggresive())
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    SetIdleStance();
                }
            } else
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    unit.GetComponent<Units.Unit>().AggresiveStance();
                }
            }
        }

        public void SetDefensiveState()
        {
            if (AllDefensive())
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    SetIdleStance();
                }
            }
            else
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    unit.GetComponent<Units.Unit>().DefensiveStance();
                }
            }
        }

        public void SetHoldGroundState()
        {
            if (AllHold())
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    SetIdleStance();
                }
            }
            else
            {
                foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
                {
                    unit.GetComponent<Units.Unit>().HoldGroundStance();
                }
            }
        }

        private bool AllAggresive()
        {
            foreach(Transform unit in InputManager.InputHandler.instance.selectedUnits )
            {
                if(unit.GetComponent<Units.Unit>().GetFiniteState() != FSM.FSMStateType.Aggressive)
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllDefensive()
        {
            foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                if (unit.GetComponent<Units.Unit>().GetFiniteState() != FSM.FSMStateType.Defensive)
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllHold()
        {
            foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                if (unit.GetComponent<Units.Unit>().GetFiniteState() != FSM.FSMStateType.HoldGround)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
