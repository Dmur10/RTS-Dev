using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Buildings
{
    public class BuildingActions : MonoBehaviour
    {
        [System.Serializable]
        public class BuildingUnits
        {
            public Units.BasicUnit[] basicUnits;  
        }
    }
}

