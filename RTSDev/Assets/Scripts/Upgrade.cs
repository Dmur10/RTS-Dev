using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public enum UpgradeType
    {
        Health, 
        Damage,
        Range,
        Speed,
        BuildTime
    }
    public class Upgrade : MonoBehaviour
    {
        public int size;
        public ScriptableObject[] targets;
        public UpgradeType upgradeType;
    }
}
