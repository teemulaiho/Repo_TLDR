using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private LayerMask groundLayerMask = new LayerMask();
    [SerializeField] private LayerMask turretLayerMask = new LayerMask();

    [SerializeField] private ParticleSystem placementSmokePrefab;
    [SerializeField] private AudioSource placementSound;

    Ray ray;
    RaycastHit hitInfo;

    private bool holdingObject;
    private GameObject grabbedGO;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();

        placementSmokePrefab = Resources.Load<ParticleSystem>("Prefabs/PlacementSmoke");

        if (placementSound == null)
            placementSound = GetComponent<AudioSource>();
    }

    public void SetGrabbedGO(GameObject newGrabbedGO)
    {
        holdingObject = true;
        grabbedGO = newGrabbedGO;
    }

    private void Update()
    {
        if (waveManager.WaveIncomingCheck()) { return; }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !holdingObject)  // Grab turtle
        {
            GrabTurret();
        }
        else if (Input.GetMouseButtonDown(0) && holdingObject)  // Drop turtle if already grabbed
        {
            DropTurret();
        }

        if (holdingObject)  // Turtle follow mouse pos
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

            holdingObject = true;
        }
    }

    public void DropTurret()
    {
        if (!holdingObject) { return; }
        if (!grabbedGO)
        {
            Turret grabbedTurret = grabbedGO.GetComponent<Turret>();
            if (!grabbedTurret.placeable) { return; }
        }

        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
        {
            grabbedGO.transform.position = hitInfo.point;

            ParticleSystem smoke = Instantiate(placementSmokePrefab);
            smoke.transform.position += hitInfo.point;
            smoke.Play();

            if (placementSound.isPlaying)
                placementSound.Stop();

            if (!placementSound.isPlaying)
                placementSound.Play();

            holdingObject = false;
        }
    }
}
