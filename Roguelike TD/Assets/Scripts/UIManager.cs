using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Turret towerDirect;
    [SerializeField] private Turret towerShotgun;

    [Space]
    [SerializeField] private Button buttonSpawnTowerDirect;
    [SerializeField] private Button buttonSpawnTowerShotgun;

    private void Awake()
    {
        if (towerDirect == null)
            towerDirect = Resources.Load<Turret>("Prefabs/TurretDirect");

        if (towerShotgun == null)
            towerShotgun = Resources.Load<Turret>("Prefabs/TurretShotgun");

        if (buttonSpawnTowerDirect == null)
            buttonSpawnTowerDirect = GameObject.Find("SpawnTowerDirect").GetComponent<Button>();

        if (buttonSpawnTowerShotgun == null)
            buttonSpawnTowerShotgun = GameObject.Find("SpawnTowerShotgun").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonSpawnTowerDirect.onClick.AddListener(SpawnTowerDirect);
        buttonSpawnTowerShotgun.onClick.AddListener(SpawnTowerShotgun);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTowerDirect()
    {
        Turret t = Instantiate(towerDirect);
        t.transform.position = GetMousePosition();
    }

    private void SpawnTowerShotgun()
    {
        Turret t = Instantiate(towerShotgun);
        t.transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float entry;
        Vector3 mousePos = Vector3.zero;

        if (plane.Raycast(ray, out entry))
        {
            mousePos = ray.GetPoint(entry);
        }

        return mousePos;
    }
}
