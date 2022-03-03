using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTSGame.Interactables
{
    public class IVehicle : Interactable
    {
        /*
        NavMeshPath path;
        int pathIter = 1;
        Vector3 AgentPosition;
        Vector3 destination =
            new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        Vector3 endDestination =
            new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        RaycastHit m_HitInfo = new RaycastHit();

        [SerializeField] Transform frontRightTransform;
        [SerializeField] Transform frontLeftTransform;

        private float currentTurnAngle = 0;
        public float maxTurnAngle = 15f;

        public NavMeshAgent agent;
        private RaycastHit hit;
        //private Vector3 destination;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = true;
            path = new NavMeshPath();
        }
        // Update is called once per frame
        void Update()
        {
            SetAgentPosition();
            if (Input.GetMouseButtonDown(0) &&
                !Input.GetKey(KeyCode.LeftShift))
            {
                var ray =
                    Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                {
                    //m_Agent.destination = m_HitInfo.point;
                    m_Path = new NavMeshPath();
                    endDestination = m_HitInfo.point;
                    m_Agent.CalculatePath(endDestination,
                                          m_Path);
                    pathIter = 1;
                    m_Agent.isStopped = false;

                }
            }

            if (m_Path.corners == null || m_Path.corners.Length == 0)
                return;


            if (pathIter >= m_Path.corners.Length)
            {
                destination =
                    new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                m_Agent.isStopped = true;
                return;
            }
            else
            {
                destination = m_Path.corners[pathIter];
            }

            if (destination.x < float.PositiveInfinity)
            {
                Vector3 direction = destination - AgentPosition;
                var newDir =
                    Vector3.RotateTowards(transform.forward,
                        direction,
                        50 * Time.deltaTime, 0.0f);
                var newRot = Quaternion.LookRotation(newDir);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation,
                                     newRot, Time.deltaTime * 2f);

                float distance =
                    Vector3.Distance(AgentPosition, destination);

                if (distance > m_Agent.radius + 0.1)
                {
                    Vector3 movement =
                        transform.forward * Time.deltaTime * 2f;

                    m_Agent.Move(movement);
                }
                else
                {
                    ++pathIter;
                    if (pathIter >= m_Path.corners.Length)
                    {
                        destination =
                            new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                        m_Agent.isStopped = true;
                    }
                }
            }
            /*
           if (Input.GetKey(KeyCode.Space))
           {
               Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               if (Physics.Raycast(ray, out hit, 100))
               {
                   destination = hit.point;
                   agent.SetDestination(destination);
               }
           }

           Vector3 relativePos = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
           Quaternion rotation = Quaternion.LookRotation(relativePos);
           transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

           Vector3 direction = (destination - transform.position).normalized;

              Vector3 transformDirection = transform.forward;
              Debug.Log(Vector3.Angle(transformDirection, direction));
              // float distance = Vector3.Distance(destination, transform.position);
              // Vector3.RotateTowards(transform.position,destination, 15f,1f);
              // Vector3 movement = transform.forward * Time.deltaTime * 10;
               //agent.Move(movement);

               /*float targetAngle = (float)getAngle(agent.desiredVelocity);
               Debug.Log(targetAngle);
               if (targetAngle > maxTurnAngle) targetAngle = maxTurnAngle;
              if (targetAngle < maxTurnAngle * -1) targetAngle = maxTurnAngle * -1;

               float acceleration = agent.acceleration;
               if (acceleration > 0)
               {
                   if (frontRight.steerAngle < targetAngle) frontRight.steerAngle -= 1f;
                   if (frontRight.steerAngle > targetAngle) frontRight.steerAngle += 1f;
                   if (frontLeft.steerAngle < targetAngle) frontLeft.steerAngle -= 1f;
                   if (frontLeft.steerAngle > targetAngle) frontLeft.steerAngle += 1f;


               }
               UpdateWheel(frontRight, frontRightTransform);
               UpdateWheel(frontLeft, frontLeftTransform);
               */
        //}
    /*
        private object getAngle(Vector3 desiredVelocity)
        {
            return Vector3.Angle(desiredVelocity, agent.velocity);
        }

        void UpdateWheel(WheelCollider col, Transform transform)
        {
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            transform.position = position;
            transform.rotation = rotation;
        }
    */
    }
}

