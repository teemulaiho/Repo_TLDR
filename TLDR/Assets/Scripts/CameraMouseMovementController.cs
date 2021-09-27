using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovementController : MonoBehaviour
{
    float x = 0f;
    float y = 0f;
    float z = 0f;

    float xDeadZone = 0.85f;
    float yDeadZone = 0.85f;

    [SerializeField] float speed = 50f;
    [SerializeField] float cameraRotateSpeed = 1f;
    [SerializeField] float scrollSpeed = 10f;
    Vector3 prevMousePos;
    Vector3 cameraForwardDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = ((Input.mousePosition.x / Screen.width) - 0.5f) * 2; 
        y = ((Input.mousePosition.y / Screen.height) - 0.5f) * 2;
        z = ((Input.mousePosition.y / Screen.height) - 0.5f) * 2;

        if (x >= xDeadZone || x <= -xDeadZone)
        {
            if (Mathf.Abs(x) <= 1)
                transform.position += new Vector3(x * speed * Time.deltaTime, transform.position.y, 0);
        }

        if (y >= yDeadZone || y <= -yDeadZone)
        {
            if (Mathf.Abs(y) <= 1)
                transform.position += new Vector3(0, transform.position.y, z * speed * Time.deltaTime);
        }

        cameraForwardDir = Camera.main.transform.forward;
        Camera.main.transform.position +=
            new Vector3(
            cameraForwardDir.x * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed,
            cameraForwardDir.y * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed,
            cameraForwardDir.z * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed
            );
    }
}
