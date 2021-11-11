using System;
using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private LayerMask groundLayerMask = new LayerMask();
    [SerializeField] private LayerMask turretLayerMask = new LayerMask();
    [SerializeField] private LayerMask spawnerLayerMask = new LayerMask();

    [SerializeField] private ParticleSystem placementSmokePrefab;
    [SerializeField] private AudioSource placementSound;

    Ray ray;
    RaycastHit hitInfo;

    public bool holdingObject;
    public GameObject grabbedGO;

    public GameObject GetGrabbedGO() { return grabbedGO; }

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

        if (holdingObject)  // Turtle follow mouse pos
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, groundLayerMask))
            {
                grabbedGO.transform.position = hitInfo.point;
            }            
        }

        if (!holdingObject && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, spawnerLayerMask))
            {
                Debug.Log("Spawner Activated");
                ActivateSpawner(hitInfo);
            }
        }
    }

    private void ActivateSpawner(RaycastHit spawnerInfo)
    {
        var spawner = spawnerInfo.transform.parent.GetComponent<Spawner>();

        if (spawner.willBeActivated)
        {
            spawner.WillBeActivatedAtRoundStart(false);
        }
        else
        {
            spawner.WillBeActivatedAtRoundStart(true);
        }
            
    }

    public void GrabTurret()
    {
        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, turretLayerMask))
        {
            grabbedGO = hitInfo.transform.parent.gameObject;
            if (hitInfo.transform.CompareTag("Tower")) grabbedGO.GetComponent<Turret>().RangeIndicator(true);

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

            if (grabbedGO.GetComponent<Turret>() != null)
            {
                grabbedGO.GetComponent<Turret>().RangeIndicator(false);
            }

            ParticleSystem smoke = Instantiate(placementSmokePrefab);
            smoke.transform.position += hitInfo.point;
            smoke.Play();
            if (grabbedGO.GetComponent<Turret>() == null)
            {
                smoke.transform.localScale = new Vector3(2, 2, 2);
            }

            if (placementSound.isPlaying)
                placementSound.Stop();

            if (!placementSound.isPlaying)
                placementSound.Play();

            holdingObject = false;
        }
    }
}
