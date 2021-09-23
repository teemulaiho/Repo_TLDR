using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float x;
    float z;

    [SerializeField] float speed = 50f;
    [SerializeField] float cameraRotateSpeed = 1f;
    [SerializeField] float scrollSpeed = 10f;
    Vector3 prevMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float dif = prevMousePos.x - Input.mousePosition.x;

            if (Mathf.Abs(dif) > 1f)
            {
                if (dif * -1 > 0)
                {
                    transform.Rotate(0, cameraRotateSpeed, 0);
                }
                else
                    transform.Rotate(0, -cameraRotateSpeed, 0);
            }     
 
            prevMousePos = Input.mousePosition;
        }

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        
        transform.position += new Vector3(x * speed * Time.deltaTime, transform.position.y, z * speed * Time.deltaTime);

        //Camera.main.fieldOfView = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }
}
