using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.InputManager
{
    public class CameraController : MonoBehaviour
    {
        public float panSpeed = 60f;
        public float scrollSpeed = 3f;

        public float topBorder = 1020f;
        public float bottomBorder = -20f;
        public float leftBorder = -20f;
        public float rightBorder = 1020f;

        public float borderThickness = 5f;
        public float minZoom = 8f;
        public float maxZoom = 20f;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {

            Vector3 pos = transform.position;

            if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderThickness) && pos.z < topBorder)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if ((Input.GetKey("s") || Input.mousePosition.y <= 0 + borderThickness) && pos.z > bottomBorder)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if ((Input.GetKey("a") || Input.mousePosition.x <= 0 + borderThickness) && pos.x > leftBorder)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
            if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderThickness) && pos.x < rightBorder)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll, minZoom, maxZoom);
        }

    }
}

