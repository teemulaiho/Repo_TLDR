using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private TurretSelection turretSelection;
    private WaveManager waveManager;
    [SerializeField] private Transform cameraTransform;

    [Space, SerializeField] private float movementSpeed = 0.175f;
    [SerializeField] private float movementTime = 10f;

    [Space, SerializeField] private Vector3 zoomAmount = new Vector3(0, -4, 4);
    [SerializeField] private float maxZoom = 5;
    [SerializeField] private float minZoom = -40;

    private bool holdingObject;

    private Vector3 newPosition;
    private Vector3 newZoom;

    [Space, SerializeReference] private bool mouseMovement = true;
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    private void Awake()
    {
        turretSelection = FindObjectOfType<TurretSelection>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start()
    {
        newPosition = transform.position;
        newZoom = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (mouseMovement) HandleMouseInput();
        HandleKeyboardInput();
        HandleZoomInput();
        LerpCamera();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
                holdingObject = turretSelection.holdingObject;

                if (!turretSelection.holdingObject && !waveManager.WaveIncomingCheck())
                {
                    turretSelection.GrabTurret();
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            Dragged();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (holdingObject && dragStartPosition == dragCurrentPosition && !waveManager.WaveIncomingCheck())
            {
                turretSelection.DropTurret();
            }
        }
    }

    public void Dragged()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float entry;

        if (plane.Raycast(ray, out entry))
        {
            dragCurrentPosition = ray.GetPoint(entry);

            newPosition = transform.position + dragStartPosition - dragCurrentPosition;
        }
    }

    private void HandleKeyboardInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
    }

    private void HandleZoomInput()
    {
        if (Input.mouseScrollDelta.y > 0 && newZoom.z < maxZoom)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0 && newZoom.z > minZoom)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
    }

    private void LerpCamera()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
