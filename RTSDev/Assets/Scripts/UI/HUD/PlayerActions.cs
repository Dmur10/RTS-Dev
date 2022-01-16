using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.UI.HUD
{
    [CreateAssetMenu(fileName ="NewPlayerActions", menuName="PlayerActions") ]
    public class PlayerActions : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]
        public List<Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();

        [Space(5)]
        [Header("Buildings")]
        [Space(15)]
        public List<Buildings.BasicBuilding> basicBuildings = new List<Buildings.BasicBuilding>();
    }
}

