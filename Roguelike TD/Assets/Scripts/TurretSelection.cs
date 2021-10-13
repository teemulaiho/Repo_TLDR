using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask = new LayerMask();
    [SerializeField] private LayerMask turretLayerMask = new LayerMask();

    Ray ray;
    RaycastHit hitInfo;

    private bool grabbedObject;
    private GameObject grabbedGO;

    public void SetGrabbedGO(GameObject newGrabbedGO)
    {
        grabbedObject = true;
        grabbedGO = newGrabbedGO;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !grabbedObject)  // Grab turtle
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, turretLayerMask))
            {
                grabbedGO = hitInfo.transform.parent.gameObject;

                grabbedObject = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && grabbedObject)  // Drop turtle if already grabbed
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
            {
                grabbedGO.transform.position = hitInfo.point;

                grabbedObject = false;
            }
        }

        if (grabbedObject)  // Turtle follow mouse pos
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
            {
                grabbedGO.transform.position = hitInfo.point;
            }            
        }
    }
}
