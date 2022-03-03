using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Interactables
{
    public class ICapturableVehicle : ICapturable
    {

        public GameObject VehiclePrefab;

        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }

        public override void capture()
        {
            Instantiate(VehiclePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
