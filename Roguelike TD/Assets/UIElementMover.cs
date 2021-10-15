using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMover : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    
    [SerializeField] private float screenHeight;
    [SerializeField] private float screenWidth;

    [SerializeField] private float prevY;
    [SerializeField] Vector3 newPos;

    [SerializeField] private float speed = 100f;
    [SerializeField] private bool isGoingUp = true;

    [SerializeField] private float speedMultiplier = 0f;

    [Space]
    [SerializeField] private Vector3 strecthAndSquash;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckScreenSize();
        Bounce();
        StretchAndSquash();
    }

    private void CheckScreenSize()
    {
        if (screenHeight != Screen.height)
            screenHeight = Screen.height;

        if (screenWidth != Screen.width)
            screenWidth = Screen.width;
    }

    private void Bounce()
    {
        Move();
    }

    public static float EaseOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }

    private void Move()
    {
        prevY = transform.position.y;
        newPos = transform.position;
        newPos.y = Mathf.Clamp(Mathf.PingPong(Time.time * speed, screenHeight), 0f + rectTransform.rect.height, screenHeight - rectTransform.rect.height);
        //speedMultiplier = EaseOutExpo(0f, screenHeight, speed);
        transform.position = newPos;

        if (prevY < transform.position.y)
            isGoingUp = true;
        else if (prevY > transform.position.y)
            isGoingUp = false;
    }

    private void StretchAndSquash()
    {
        if (isGoingUp)
        {
            float distToTop = screenHeight - transform.position.y - rectTransform.rect.height;
            Debug.Log(distToTop);

            strecthAndSquash = transform.localScale;
            strecthAndSquash.x = Mathf.Clamp01(Mathf.Abs((distToTop / 20f) - screenHeight));
        }
    }
}
