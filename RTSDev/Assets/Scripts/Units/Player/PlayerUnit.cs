using System;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units.Player
{
    public class PlayerUnit : Unit
    {

        public State state = State.Idle;

        private bool hasAggro = false;

        private void Start()
        {
            base.Start();
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
        }

        private void Update()
        {
            switch(state)
            {
                case State.Idle:
                    //checkForTarget();
                    break;
                case State.Moving:
                    break;
                case State.Attacking:
                    Attack();
                    MoveToTarget();
                    break;
                case State.Aggresive:
                    //check for target if have one chase it so like attacking but continuous till target dead or lost;
                    break;
                case State.Defensive:
                    //check for target chase for a while and give up or has been killed.
                    break;
                case State.StandGround:
                    //only attack enemy in range and do not move.
                    break;
            }
            atkCooldown -= Time.deltaTime;
            if (hasAggro)
            {
                Attack();
                MoveToTarget();
            }
        }

        // Update is called once per frame
        public void MoveUnit(Vector3 destination)
        {
            target = null;
            state = State.Idle;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }
            navAgent.SetDestination(destination);
        }

        public void MoveUnit(Vector3 destination, float v, Action p)
        {
            target = null;
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }

            if (Vector3.Distance(transform.position, destination) < v)
            {
                navAgent.SetDestination(transform.position);
                p.Invoke();
            }
            else
            {
                navAgent.SetDestination(destination);
            }
        }

        public bool IsIdle()
        {
            if(navAgent.velocity.magnitude <= 0)
            {
                return true;
            }
            return false;
        }

        public void SetTarget(Transform tf)
        {
            target = tf;
            targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
            hasAggro = true;
        }

        public override bool CheckForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < colliders.Length;)
            {
                target = colliders[i].gameObject.transform;
                targetStatDisplay = target.gameObject.GetComponentInChildren<StatDisplay>();
                return true;
            }
            return false;
        }
        

    }
}

