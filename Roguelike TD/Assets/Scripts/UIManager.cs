using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform turretParent;
    [SerializeField] private TurretSelection turretSelectionScript;

    [Space]
    [SerializeField] private Turret towerDirect;
    [SerializeField] private Turret towerShotgun;

    [Header("3 Options Stuff")]
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private Button turretButton1;
    [SerializeField] private Button turretButton2;
    [SerializeField] private Button turretButton3;
    [SerializeField] private TMP_Text turretButton1Text;
    [SerializeField] private TMP_Text turretButton2Text;
    [SerializeField] private TMP_Text turretButton3Text;

    [Space]
    [SerializeField] private int maxTurretCount = 3;
    [SerializeField] private int curTurretCount = 0;
    [SerializeField] private TMP_Text shellText;
    [SerializeField] private TMP_Text waveCountdownText;

    private Turret turretOption1;
    private Turret turretOption2;
    private Turret turretOption3;

    private void Awake()
    {
        if (turretParent == null)
            turretParent = GameObject.Find("TURRETPARENT").transform;

        if (turretSelectionScript == null)
            turretSelectionScript = GameObject.Find("TurretSelectionManager").GetComponent<TurretSelection>();

        if (towerDirect == null)
            towerDirect = Resources.Load<Turret>("Prefabs/Turrets/TurretDirect");

        if (towerShotgun == null)
            towerShotgun = Resources.Load<Turret>("Prefabs/Turrets/TurretShotgun");
        
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");

        if (turretButton1 == null)
            turretButton1 = GameObject.Find("SpawnTowerOption1").GetComponent<Button>();

        if (turretButton2 == null)
            turretButton2 = GameObject.Find("SpawnTowerOption2").GetComponent<Button>();

        if (turretButton3 == null)
            turretButton3 = GameObject.Find("SpawnTowerOption3").GetComponent<Button>();

        if (shellText == null)
            shellText = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        turretButton1.onClick.AddListener(TurretButton1);
        turretButton2.onClick.AddListener(TurretButton2);
        turretButton3.onClick.AddListener(TurretButton3);

        shellText.text = "Turrets left: " + maxTurretCount.ToString();
    }

    public void ActivateTurretButtons(GameObject option1, GameObject option2, GameObject option3)
    {
        turretOption1 = option1.GetComponent<Turret>();
        turretOption2 = option2.GetComponent<Turret>();
        turretOption3 = option3.GetComponent<Turret>();

        UpdateButtonTexts();

        buttonsParent.SetActive(true);
    }

    private void UpdateButtonTexts()
    {
        turretButton1Text.text = $"<b>{turretOption1.GetTurretName()}</b>\n{turretOption1.GetTurretDescription()}";
        turretButton2Text.text = $"<b>{turretOption2.GetTurretName()}</b>\n{turretOption2.GetTurretDescription()}";
        turretButton3Text.text = $"<b>{turretOption3.GetTurretName()}</b>\n{turretOption3.GetTurretDescription()}";
    }

    private void TurretButton1() { SpawnTurret(turretOption1); }
    private void TurretButton2() { SpawnTurret(turretOption2); }
    private void TurretButton3() { SpawnTurret(turretOption3); }

    private void SpawnTurret(Turret turretToSpawn)
    {
        Turret t = Instantiate(turretToSpawn);
        turretSelectionScript.SetGrabbedGO(t.transform.gameObject);
        //UpdateTurretCount();

        buttonsParent.SetActive(false);

        // Tell gameManager what you picked so it can be removed from the pool
    }
    
    /*
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
            turretButton1.interactable = false;
            turretButton2.interactable = false;
            turretButton3.interactable = false;
        }
    }*/

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        waveCountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}