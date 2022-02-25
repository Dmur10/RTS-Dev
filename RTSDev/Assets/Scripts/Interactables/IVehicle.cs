using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTSGame.Interactables
{
    public class IVehicle : MonoBehaviour
    {
        [SerializeField] WheelCollider frontRight;
        [SerializeField] WheelCollider frontLeft;
        [SerializeField] WheelCollider RearRight;
        [SerializeField] WheelCollider RearLeft;

        public float acceleration = 500f;
        public float brakingForce = 300f;
        public float maxTurnAngle = 15f;

        private float currentAcceleration = 0;
        private float currentBrakingForce = 0;
        private float currentTurnAngle = 0;
        // Update is called once per frame
        void Update()
        {

            currentAcceleration = acceleration * Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space))
            {
                currentBrakingForce = brakingForce;
            } else
            {
                currentBrakingForce = 0;
            }

            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque = currentAcceleration;

            frontRight.brakeTorque = currentBrakingForce;
            frontLeft.brakeTorque = currentBrakingForce;
            RearRight.brakeTorque = currentBrakingForce;
            RearLeft.brakeTorque = currentBrakingForce;

            currentTurnAngle = maxTurnAngle*Input.GetAxis("Horizontal");
            frontRight.steerAngle = currentTurnAngle;
            frontLeft.steerAngle = currentTurnAngle;
        }
    }
}

