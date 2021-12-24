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
        public bool isPlayerUnit;
        public unitTye type;

        public string unitName;

        public GameObject HumanPrefab;
        public GameObject BurnedPrefab;

        public int cost;
        public int damage;
        public int health;

    }
}

