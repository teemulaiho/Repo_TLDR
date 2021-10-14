using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private LayerMask groundLayerMask = new LayerMask();
    [SerializeField] private LayerMask turretLayerMask = new LayerMask();

    Ray ray;
    RaycastHit hitInfo;

    private bool grabbedObject;
    private GameObject grabbedGO;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    public void SetGrabbedGO(GameObject newGrabbedGO)
    {
        grabbedObject = true;
        grabbedGO = newGrabbedGO;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !grabbedObject && !waveManager.WaveIncomingCheck())  // Grab turtle
        {
            GrabTurret();
        }
        else if (Input.GetMouseButtonDown(0) && grabbedObject && !waveManager.WaveIncomingCheck())  // Drop turtle if already grabbed
        {
            DropTurret();
        }

        if (grabbedObject)  // Turtle follow mouse pos
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
            {
                grabbedGO.transform.position = hitInfo.point;
            }            
        }
    }

    private void GrabTurret()
    {
        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, turretLayerMask))
        {
            grabbedGO = hitInfo.transform.parent.gameObject;

            grabbedObject = true;
        }
    }

    public void DropTurret()
    {
        if (!grabbedObject) { return; }

        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
        {
            grabbedGO.transform.position = hitInfo.point;

            grabbedObject = false;
        }
    }
}
