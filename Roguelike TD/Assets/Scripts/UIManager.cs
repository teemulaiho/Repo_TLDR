using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform turretParent;

    [Space]
    [SerializeField] private Turret towerDirect;
    [SerializeField] private Turret towerShotgun;

    [Space]
    [SerializeField] private Button buttonSpawnTowerDirect;
    [SerializeField] private Button buttonSpawnTowerShotgun;

    [Space]
    [SerializeField] private int maxTurretCount = 3;
    [SerializeField] private int curTurretCount = 0;
    [SerializeField] private TMP_Text shellText;

    private void Awake()
    {
        if (turretParent == null)
            turretParent = GameObject.Find("TURRETPARENT").transform;

        if (towerDirect == null)
            towerDirect = Resources.Load<Turret>("Prefabs/TurretDirect");

        if (towerShotgun == null)
            towerShotgun = Resources.Load<Turret>("Prefabs/TurretShotgun");

        if (buttonSpawnTowerDirect == null)
            buttonSpawnTowerDirect = GameObject.Find("SpawnTowerDirect").GetComponent<Button>();

        if (buttonSpawnTowerShotgun == null)
            buttonSpawnTowerShotgun = GameObject.Find("SpawnTowerShotgun").GetComponent<Button>();

        if (shellText == null)
            shellText = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonSpawnTowerDirect.onClick.AddListener(SpawnTowerDirect);
        buttonSpawnTowerShotgun.onClick.AddListener(SpawnTowerShotgun);

        shellText.text = "Turrets left: " + maxTurretCount.ToString();
    }

    private void SpawnTowerDirect()
    {
        Turret t = Instantiate(towerDirect);
        SetTurretPosition(t);
        UpdateTurretCount();
    }

    private void SpawnTowerShotgun()
    {
        Turret t = Instantiate(towerShotgun);
        SetTurretPosition(t);
        UpdateTurretCount();
    }

    private void SetTurretPosition(Turret t)
    {
        t.transform.SetParent(turretParent);
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

    private void UpdateTurretCount()
    {
        curTurretCount++;

        shellText.text = "Turrets left: " + (maxTurretCount - curTurretCount).ToString();

        if (curTurretCount == maxTurretCount)
        {
            buttonSpawnTowerDirect.interactable = false;
            buttonSpawnTowerShotgun.interactable = false;
        }
    }
}
