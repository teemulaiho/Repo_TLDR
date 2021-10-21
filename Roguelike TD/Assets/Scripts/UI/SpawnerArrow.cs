using UnityEngine;

public class SpawnerArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowGO;
    private Transform uiParent;

    private Vector3 targetPos;
    private RectTransform pointerRectTransform;

    private float borderSize = 90f;

    private void Awake()
    {
        uiParent = GameObject.Find("Canvas").transform;
        pointerRectTransform = Instantiate(arrowGO, uiParent).GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Rotation
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 fromPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);//pointerRectTransform.transform.position;
        Vector3 dir = (targetPos - fromPos);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        // Position
        bool isOffScreen = targetPos.x <= 0 || targetPos.x >= Screen.width || targetPos.y <= 0 || targetPos.y >= Screen.height;
        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPos = targetPos;
            if (cappedTargetScreenPos.x <= 0) cappedTargetScreenPos.x = 0f + borderSize;
            if (cappedTargetScreenPos.x >= Screen.width) cappedTargetScreenPos.x = Screen.width - borderSize;
            if (cappedTargetScreenPos.y <= 0) cappedTargetScreenPos.y = 0f + borderSize;
            if (cappedTargetScreenPos.y >= Screen.height) cappedTargetScreenPos.y = Screen.height - borderSize;

            pointerRectTransform.position = cappedTargetScreenPos;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            pointerRectTransform.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (pointerRectTransform != null)
            Destroy(pointerRectTransform.gameObject);
    }

    private void OnDisable()
    {
        if (pointerRectTransform != null)
            Destroy(pointerRectTransform.gameObject);
    }
}
