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
                unit.GetComponent<Units.Unit>().SetFiniteState(FSM.FSMStateType.Idle);
            }
        }

        public void SetAggressiveState()
        {
            foreach( Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                unit.GetComponent<Units.Unit>().AggresiveStance();
            }
        }

        public void SetDefensiveState()
        {
            foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                unit.GetComponent<Units.Unit>().DefensiveStance();
            }
        }

        public void SetHoldGroundState()
        {
            foreach (Transform unit in InputManager.InputHandler.instance.selectedUnits)
            {
                unit.GetComponent<Units.Unit>().HoldGroundStance();
            }
        }
    }
}
