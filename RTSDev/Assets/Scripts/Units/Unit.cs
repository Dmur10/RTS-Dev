using RTSGame.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Moving,
            Attacking
        }

        public State state = State.Idle;

        protected NavMeshAgent navAgent;
        public BasicUnit unitType;

        public UnitStatTypes.Base baseStats;
        public UnitStatDisplay statDisplay;

        [SerializeField] protected Transform target = null;
        protected StatDisplay targetStatDisplay;

        public float atkCooldown;
        protected float distance;

        public void Start()
        {
            baseStats = unitType.baseStats;
            atkCooldown = baseStats.atkSpeed;
            navAgent = GetComponent<NavMeshAgent>();
        }

        protected void Attack()
        {
            if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
            {
                targetStatDisplay.takeDamage(baseStats.damage);
                atkCooldown = baseStats.atkSpeed;
            }
        }
    }

}
