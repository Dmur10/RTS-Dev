using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Upgrades
{
    public enum UpgradeType
    {
        Health, 
        Damage,
        Range,
        Speed,
        BuildTime
    }

    [CreateAssetMenu(fileName = "New Upgrade", menuName = "New Upgrade/Basic")]
    public class BasicUpgrade : ScriptableObject
    {
        public int size;
        public Units.BasicUnit[] units;
        public Buildings.BasicBuilding[] buildings;
        public UpgradeType upgradeType;
    }
}
