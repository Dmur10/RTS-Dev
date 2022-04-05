using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.FSM
{
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED,
    }

    public enum FSMStateType {
        Idle,
        Patrol,
        Chase,
        MoveToDestination,
        Attack,
        Aggressive,
        Defensive,
        HoldGround,
        MoveToBuildZone,
        Building,
        MoveToResource,
        GatherResource,
        MoveToStorage,
        MoveToCapturePoint,
        Capture
    }
    public abstract class AbstractFSMState : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;
        protected Units.Unit unit;
        protected FiniteStateMachine fsm;

        public ExecutionState ExecutionState { get; protected set; }
        public FSMStateType StateType { get; protected set; }
        public bool EnteredState { get; protected set; }

        public virtual void OnEnable()
        {
            ExecutionState = ExecutionState.NONE;
        }

        public virtual bool EnterState()
        {
            bool successNavMesh = true;
            bool successUnit = true;
            bool successFSM = true;
            ExecutionState = ExecutionState.ACTIVE;

            successNavMesh = (navMeshAgent != null);
            successUnit = (unit != null);
            successFSM = (fsm != null);

            return successNavMesh && successUnit && successFSM;
        }

        public abstract void UpdateState();

        public virtual bool ExitState()
        {
            ExecutionState = ExecutionState.COMPLETED;
            return true;
        }

        public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            if(navMeshAgent != null)
            {
                this.navMeshAgent = navMeshAgent;
            }
        }

        public virtual void SetExecutingFSM(FiniteStateMachine fsm)
        {
            if(fsm != null)
            {
                this.fsm = fsm;
            }
        }

        public virtual void SetExecutingUnit(Units.Unit unit)
        {
            if(unit != null)
            {
                this.unit = unit;
            }
        }
    }
}

