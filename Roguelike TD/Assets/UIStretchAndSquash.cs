using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStretchAndSquash : MonoBehaviour
{
    RectTransform rect;
    
    Vector3 prvMousePos;
    Vector3 curMousePos;

    //      Scale Max   Scale Min
    //  x	1.8818	    0.650357
    //  y	1.53762	    0.531582
    [SerializeField] float followSpeed = 1f;
    Vector3 newPos;

    [SerializeField] float squashSpeed = 1f;
    [SerializeField] float squashReturnSpeed = 1f;

    float xOrgScale = 1f;
    float xNewScale = 1f;
    float yOrgScale = 1f;
    float yNewScale = 1f;
    
    [Space]
    [SerializeField] float maxScaleX = 1.8818f;
    [SerializeField] float minScaleX = 0.650357f;
    [SerializeField] float maxScaleY = 1.53762f;
    [SerializeField] float minScaleY = 0.531582f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curMousePos = Input.mousePosition;

        float distX = Mathf.Abs(curMousePos.x - prvMousePos.x); 
        float distY = Mathf.Abs(curMousePos.y - prvMousePos.y);
  
        // Squash
        if (distX > 0f)
        {
            xNewScale = Mathf.Lerp(rect.localScale.x, maxScaleX, Time.deltaTime * distX *squashSpeed);
        }
        else
        {
            xNewScale = Mathf.Lerp(rect.localScale.x, xOrgScale, Time.deltaTime * squashReturnSpeed);
        }

        // Stretch
        if (distY > 0)
        {
            yNewScale = Mathf.Lerp(rect.localScale.y, maxScaleY, Time.deltaTime * distY * squashSpeed);
        }
        else
        {
            yNewScale = Mathf.Lerp(rect.localScale.y, yOrgScale, Time.deltaTime * squashReturnSpeed);
        }

        newPos.x = Mathf.Lerp(prvMousePos.x, curMousePos.x, Time.deltaTime * followSpeed);
        newPos.y = Mathf.Lerp(prvMousePos.y, curMousePos.y, Time.deltaTime * followSpeed);
        newPos.z = transform.position.z;

        transform.position = newPos;
        rect.localScale = new Vector2(xNewScale, yNewScale);

        prvMousePos = curMousePos;
    }
}
