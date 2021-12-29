using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Globals
    {

        public static BuildingBluePrint[] BUILDING_DATA = new BuildingBluePrint[]
        {
        new BuildingBluePrint("building", 100,100)
        };
        
    public static int TERRAIN_LAYER_MASK = 1 << 8;
}
