using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMoverScript : MonoBehaviour
{
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private bool isMovingUp = false;
    [SerializeField] private float speed = 4f;

    [SerializeField] private float scaleMinX = 0.1f;
    [SerializeField] private float scaleMinY = 0.1f;
    [SerializeField] private float scaleMaxX = 5.0f;
    [SerializeField] private float scaleMaxY = 5.0f;
     

    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingUp = !isMovingUp;
        }


        float y = transform.position.y;


        if (isMovingUp)
        {
            float newY = Mathf.Lerp(transform.position.y, Screen.height - rectTrans.rect.height / 2, Time.deltaTime * speed);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            float newY = Mathf.Lerp(transform.position.y, 0 + rectTrans.rect.height / 2, Time.deltaTime * speed);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        float difY = Mathf.Abs(y - transform.position.y);


        float distToTarget = 0f;

        if (isMovingUp)
            distToTarget = Screen.height - transform.position.y;
        else
            distToTarget = transform.position.y;


        //if (difY > speed)
        //{
        //    transform.localScale = new Vector3(transform.localScale.x / 2f, difY / 10f);
        //}
        if (distToTarget > rectTrans.rect.height * 2)
        {
            float xScale = Mathf.Lerp(transform.localScale.x, 0.2f, Time.deltaTime * difY);
            float yScale = Mathf.Lerp(transform.localScale.y, 2f, Time.deltaTime * difY);

            transform.localScale = new Vector3(xScale, yScale);
        }
        else
        {
            float xScale = Mathf.Lerp(transform.localScale.x, 1, Time.deltaTime * speed);
            float yScale = Mathf.Lerp(transform.localScale.y, 1, Time.deltaTime * speed);

            transform.localScale = new Vector3(xScale, yScale);
        }


        //if (Mathf.Abs(y - transform.position.y) > 0.1f)
        //{
        //    float difY = y - transform.position.y;

        //    float xScale = 1/difY;
        //    float yScale = difY;

        //    if (xScale < scaleMinX)
        //    {
        //        xScale = scaleMinX;
        //    }
        //    else if (transform.localScale.x > scaleMaxX)
        //    {
        //        xScale = scaleMaxX;
        //    }
        //    else if (transform.localScale.y < scaleMinY)
        //    {
        //        yScale = scaleMinY;
        //    }
        //    else if (transform.localScale.y > scaleMaxY)
        //    {
        //        yScale = scaleMaxY;
        //    }

        //    //transform.localScale = new Vector3(xScale, yScale);
        //}
    }
}
