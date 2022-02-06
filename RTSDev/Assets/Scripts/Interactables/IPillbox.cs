using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class IPillbox : IBuilding
    {
        private Collider[] colliders;
        [SerializeField]private Transform target;
        private Units.UnitStatDisplay targetUnit;

        private bool hasAggro = false;
        private float atkCooldown;

        public float range = 10;
        public float damage = 25;
        public float atkSpeed = 4;

        // Update is called once per frame
        void Update()
        {
            atkCooldown -= Time.deltaTime;
            if(!hasAggro)
            {
                checkForTarget();
            } else
            {
                Attack();
            }
        }

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
            OnDrawGizmosSelected();
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void checkForTarget()
        {
            colliders = Physics.OverlapSphere(transform.position, range, Units.UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < colliders.Length;)
            {
                target = colliders[i].gameObject.transform;
                targetUnit = target.gameObject.GetComponentInChildren<Units.UnitStatDisplay>();
                hasAggro = true;
                break;
            }
        }

        private void Attack()
        {
            if (atkCooldown <= 0)
            {
                targetUnit.takeDamage(damage);
                atkCooldown = atkSpeed;
            }
        }
    }
}

