using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Units
{
    [CreateAssetMenu(fileName ="New Unit", menuName ="New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum unitTye
        {
            worker,
            scaveger,
            warrior
        }

        [Space(15)]
        [Header("Unit Settings")]

        public unitTye type;
        public string unitName;
        public GameObject HumanPrefab;
        public GameObject BurnedPrefab;
        public float SpawnTime;

        [Space(15)]
        [Header("Unit Stats")]
        [Space(40)]

        public UnitStatTypes.Base baseStats;

    }
}

