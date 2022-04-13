using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]
        AbstractFSMState startingState;
        [SerializeField]
        AbstractFSMState currentState;

        [SerializeField]
        List<AbstractFSMState> validStates;
        Dictionary<FSMStateType, AbstractFSMState> fsmStates;

        public void Awake()
        {
            currentState = null;

            fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            Units.Unit unit = GetComponent<Units.Unit>();
            GetComponents(validStates);

            foreach (AbstractFSMState state in validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingUnit(unit);
                state.SetNavMeshAgent(navMeshAgent);
                fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            if(startingState != null)
            {
                EnterState(startingState);
            }
        }

        public void Update()
        {
            if(currentState != null)
            {
                currentState.UpdateState();
            }
        }

        public FSMStateType GetCurrentState()
        {
            return currentState.StateType;
        }

        public void EnterState(AbstractFSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }

            if(currentState != null){
                currentState.ExitState();
            }
            currentState = nextState;
            currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            if (fsmStates.ContainsKey(stateType))
            {
                AbstractFSMState nextState = fsmStates[stateType];

                EnterState(nextState);
            }
        }
    }
}

