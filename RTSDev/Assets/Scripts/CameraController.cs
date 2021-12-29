using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float scrollSpeed = 5f;

    public float borderThickness = 10f;
    public float minY = 10f;
    public float maxY = 100f;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;

        if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderThickness) && pos.z < startPos.z + 20)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        } 
        if ((Input.GetKey("s") || Input.mousePosition.y <= 0 + borderThickness) && pos.z > startPos.z - 180)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("a") || Input.mousePosition.x <= 0 + borderThickness) && pos.x > startPos.x - 15)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        } 
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderThickness) && pos.x < startPos.x + 175)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos = transform.position;

        pos.y -= scroll * 2000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

}
