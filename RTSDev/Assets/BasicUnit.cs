using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Units
{
    [CreateAssetMenu(fileName ="New Unit", menuName ="New Unit/Basic")]
    public class Unit : ScriptableObject
    {
        public enum unitTye
        {
            Worker,
            Scaveger,
            Warrior
        }

        [Space(15)]
        [Header("Unit Settings")]

        public unitTye type;
        public string unitName;
        public GameObject HumanPrefab;
        public GameObject BurnedPrefab;

        [Space(15)]
        [Header("Unit Stats")]
        [Space(40)]
        public int cost;
        public int damage;
        public int atkRange;
        public int health;

    }
}

