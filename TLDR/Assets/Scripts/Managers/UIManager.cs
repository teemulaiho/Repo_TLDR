using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    DebugManager debugManager;
    public Canvas UIprefab;
    Canvas ui;

    GameObject selectedObject;

    GameObject xpObj;
    TMP_Text xpTxt;

    GameObject strObj;
    TMP_Text strTxt;

    GameObject spdObj;
    TMP_Text spdTxt;

    GameObject rngObj;
    TMP_Text rngTxt;

    GameObject ammObj;
    TMP_Text ammTxt;

    GameObject timObj;
    TMP_Text timTxt;

    GameObject eneObj;
    TMP_Text eneTxt;

    GameObject spaObj;
    TMP_Text spaTxt;

    TMP_Text castlePurchaseText;

    [SerializeField] List<Button> uiButtons;
    [SerializeField] List<Slider> uiSliders;

    Button strengthButton;
    Button speedButton;
    Button rangeButton;
    Button ammoButton;

    Button castleSpawnButton;

    Button changeTowerBulletTypeToDirectButton;
    Button changeTowerBulletTypeToConeButton;
    Button changeTowerBulletTypeToAOEButton;

    TMP_Text mouseCameraControlButtonTxt;
    Button mouseCameraControlButton;
    GameObject cameraController;

    Slider sliderSpawnPoint;
    TMP_Text txtSpawnPoint;

    Slider sliderEnemySpawnRate;
    TMP_Text txtEnemySpawnRate;

    Slider sliderEnemyHealth;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
        debugManager = gameManager.GetDebugManager();
    }

    private void Awake()
    {
        ui = Instantiate(UIprefab);

        cameraController = GameObject.Find("CameraMovement");
        uiButtons = new List<Button>();
        uiSliders = new List<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int childCount = ui.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (ui.transform.GetChild(i).name == "Experience")
            {
                xpObj = ui.transform.GetChild(i).gameObject;
                xpTxt = xpObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Strength")
            {
                strObj = ui.transform.GetChild(i).gameObject;
                strTxt = strObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Speed")
            {
                spdObj = ui.transform.GetChild(i).gameObject;
                spdTxt = spdObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Range")
            {
                rngObj = ui.transform.GetChild(i).gameObject;
                rngTxt = rngObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Ammo")
            {
                ammObj = ui.transform.GetChild(i).gameObject;
                ammTxt = ammObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Time")
            {
                timObj = ui.transform.GetChild(i).gameObject;
                timTxt = timObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "Enemies")
            {
                eneObj = ui.transform.GetChild(i).gameObject;
                eneTxt = eneObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "SpawnTimer")
            {
                spaObj = ui.transform.GetChild(i).gameObject;
                spaTxt = spaObj.GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "ButtonStrength")
            {
                strengthButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                strengthButton.onClick.AddListener(IncreaseStrength);
            }
            else if (ui.transform.GetChild(i).name == "ButtonSpeed")
            {
                speedButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                speedButton.onClick.AddListener(IncreaseSpeed);
            }
            else if (ui.transform.GetChild(i).name == "ButtonRange")
            {
                rangeButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                rangeButton.onClick.AddListener(IncreaseRange);
            }
            else if (ui.transform.GetChild(i).name == "ButtonAmmo")
            {
                ammoButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                ammoButton.onClick.AddListener(IncreaseAmmo);
            }
            else if (ui.transform.GetChild(i).name == "ButtonSpawnCastle")
            {
                castleSpawnButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                castlePurchaseText = castleSpawnButton.transform.Find("PurchaseCost").GetComponent<TMP_Text>();
                castleSpawnButton.onClick.AddListener(SpawnCastle);
            }
            else if (ui.transform.GetChild(i).name == "ButtonChangeBulletTypeDirect")
            {
                changeTowerBulletTypeToDirectButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                changeTowerBulletTypeToDirectButton.onClick.AddListener(ChangeTowerBulletToDirect);
            }
            else if (ui.transform.GetChild(i).name == "ButtonChangeBulletTypeCone")
            {
                changeTowerBulletTypeToConeButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                changeTowerBulletTypeToConeButton.onClick.AddListener(ChangeTowerBulletToCone);
            }
            else if (ui.transform.GetChild(i).name == "ButtonChangeBulletTypeAOE")
            {
                changeTowerBulletTypeToAOEButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                changeTowerBulletTypeToAOEButton.onClick.AddListener(ChangeTowerBulletToAOE);
            }
            else if (ui.transform.GetChild(i).name == "CameraControl")
            {
                mouseCameraControlButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                mouseCameraControlButton.onClick.AddListener(ChangeCameraControl);

                mouseCameraControlButtonTxt = mouseCameraControlButton.GetComponentInChildren<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "SliderSpawnPoint")
            {
                sliderSpawnPoint = ui.transform.GetChild(i).gameObject.GetComponent<Slider>();
                txtSpawnPoint = sliderSpawnPoint.transform.Find("TxtValue").GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "SliderEnemySpawnRate")
            {
                sliderEnemySpawnRate = ui.transform.GetChild(i).gameObject.GetComponent<Slider>();
                txtEnemySpawnRate = sliderEnemySpawnRate.transform.Find("TxtValue").GetComponent<TMP_Text>();
            }
            else if (ui.transform.GetChild(i).name == "SliderEnemyHealth")
            {
                sliderEnemyHealth = ui.transform.GetChild(i).gameObject.GetComponent<Slider>();
            }
        }

        uiButtons.AddRange(ui.GetComponentsInChildren<Button>());

        if (uiButtons != null &&
            uiButtons.Count > 0)
        {
            foreach (Button b in uiButtons)
            {
                // For reference, delete comment once done.
                //castleSpawnButton = ui.transform.GetChild(i).gameObject.GetComponent<Button>();
                //castlePurchaseText = castleSpawnButton.transform.Find("PurchaseCost").GetComponent<TMP_Text>();
                //castleSpawnButton.onClick.AddListener(SpawnCastle);

                if (b.name == "ButtonSpawnTurretDirect")
                {
                    b.onClick.AddListener(SpawnDirectTurret);
                }
                else if (b.name == "ButtonSpawnTurretCone")
                {
                    b.onClick.AddListener(SpawnConeTurret);
                }
                else if (b.name == "ButtonSpawnTurretAOE")
                {
                    b.onClick.AddListener(SpawnAOETurret);
                }
            }
        }

        uiSliders.AddRange(ui.GetComponentsInChildren<Slider>());
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtons();
        CheckSliders();

        GetPlayerInput();
        SetUI();
    }

    private void CheckSliders()
    {
        gameManager.SetTimer(sliderSpawnPoint.name, sliderSpawnPoint.value);
        txtSpawnPoint.text = sliderSpawnPoint.value.ToString();

        gameManager.SetTimer(sliderEnemySpawnRate.name, sliderEnemySpawnRate.value);
        txtEnemySpawnRate.text = sliderEnemySpawnRate.value.ToString();

        gameManager.SetTimer(sliderEnemyHealth.name, sliderEnemyHealth.value);





        if (uiSliders != null &&
            uiSliders.Count > 0)
        {
            foreach (Slider s in uiSliders)
            {
                if (s.name == "SliderDirectBulletDamage")
                {
                    debugManager.SetValue((int)s.value, true, BulletManager.BulletType.Direct);
                }
                else if (s.name == "SliderDirectBulletSpeed")
                {
                    debugManager.SetValue((int)s.value, false, BulletManager.BulletType.Direct);
                }
                else if (s.name == "SliderConeBulletDmage")
                {
                    debugManager.SetValue((int)s.value, true, BulletManager.BulletType.Cone);
                }
                else if (s.name == "SliderConeBulletSpeed")
                {
                    debugManager.SetValue((int)s.value, false, BulletManager.BulletType.Cone);
                }
                else if (s.name == "SliderAOEBulletDamage")
                {
                    debugManager.SetValue((int)s.value, true, BulletManager.BulletType.AOE);
                }
                else if (s.name == "SliderAOEBulletSpeed")
                {
                    debugManager.SetValue((int)s.value, false, BulletManager.BulletType.AOE);
                }
            }
        }


    }

    private void SetUI()
    {
        xpTxt.text = gameManager.GetPlayerExperience().ToString();
        strTxt.text = gameManager.GetPlayerStrength().ToString();
        spdTxt.text = gameManager.GetPlayerSpeed().ToString();
        rngTxt.text = gameManager.GetCastleRange().ToString();
        ammTxt.text = gameManager.GetCastleAmmo().ToString();

        timTxt.text = ((int)gameManager.GetElapsedTime()).ToString();
        eneTxt.text = ((int)gameManager.GetEnemies().Count).ToString();
        spaTxt.text = ((int)gameManager.GetTimeLeftUntilNextEnemySpawnPoint()).ToString();

        castlePurchaseText.text = gameManager.GetNewTowerPurchasePrice().ToString();
    }

    private void IncreaseStrength()
    {
        gameManager.Upgrade(UpgradeManager.UpgradeType.Strength);
    }

    private void IncreaseSpeed()
    {
        gameManager.Upgrade(UpgradeManager.UpgradeType.Speed);
    }

    private void IncreaseRange()
    {
        gameManager.Upgrade(UpgradeManager.UpgradeType.Range);
    }

    private void IncreaseAmmo()
    {
        gameManager.Upgrade(UpgradeManager.UpgradeType.Ammo);
    }

    private void IncreaseTowerCount()
    {
        gameManager.Upgrade(UpgradeManager.UpgradeType.NewTower);
    }

    private void SpawnCastle()
    {
        gameManager.Spawn(PlayerManager.PlayerStructures.Castle);
        IncreaseTowerCount();
    }

    private void SpawnDirectTurret()
    {
        gameManager.Spawn(PlayerManager.PlayerStructures.TowerDirect);
    }

    private void SpawnConeTurret()
    {
        gameManager.Spawn(PlayerManager.PlayerStructures.TowerCone);
    }

    private void SpawnAOETurret()
    {
        gameManager.Spawn(PlayerManager.PlayerStructures.TowerAOE);
    }

    private void CheckButtons()
    {
        //CheckStrengthUpgrade();
        //CheckSpeedUpgrade();
        //CheckRangeUpgrade();
        //CheckAmmoUpgrade();

        CheckUpgradeType(UpgradeManager.UpgradeType.Strength, strengthButton);
        CheckUpgradeType(UpgradeManager.UpgradeType.Speed, speedButton);
        CheckUpgradeType(UpgradeManager.UpgradeType.Range, rangeButton);
        CheckUpgradeType(UpgradeManager.UpgradeType.Ammo, ammoButton);
        CheckUpgradeType(UpgradeManager.UpgradeType.NewTower, castleSpawnButton);

        CheckBulletType(changeTowerBulletTypeToDirectButton);
        CheckBulletType(changeTowerBulletTypeToConeButton);
        CheckBulletType(changeTowerBulletTypeToAOEButton);
    }

    private void CheckUpgradeType(UpgradeManager.UpgradeType type, Button button)
    {
        int playerXP = gameManager.GetPlayerExperience();
        int upgradeCost = gameManager.GetUpgradeCost(type);

        if (playerXP >= upgradeCost)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            button.GetComponentInChildren<TMP_Text>().text = "Add " + type.ToString() + " (" + upgradeCost + ")";
        }
    }

    private void CheckBulletType(Button button)
    {
        if (selectedObject != null)
        {
            if (selectedObject.name == "Castle")
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else
            button.interactable = false;
    }

    private void ChangeTowerBulletToDirect()
    {
        if (selectedObject != null)
            gameManager.SetTowerBulletType(BulletManager.BulletType.Direct, selectedObject);
    }

    private void ChangeTowerBulletToCone()
    {
        if (selectedObject != null)
            gameManager.SetTowerBulletType(BulletManager.BulletType.Cone, selectedObject);
    }

    private void ChangeTowerBulletToAOE()
    {
        if (selectedObject != null)
            gameManager.SetTowerBulletType(BulletManager.BulletType.AOE, selectedObject);
    }

    private void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hitInfo = new RaycastHit();
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            // First deselect current object.
            if (selectedObject != null)
            {
                DeselectObject(selectedObject);
                selectedObject = null;
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform != null)
                {
                    SelectObject(hitInfo.transform.gameObject, 0);
                }
            }          
        }
    }

    private void SelectObject(GameObject obj, int mouseButton)
    {
        if (obj.tag == "Castle")
        {
            selectedObject = obj.transform.gameObject;
            gameManager.SelectObject(selectedObject, mouseButton);
        }
        else if (obj.tag == "EnemySpawnPoint")
        {
            selectedObject = obj.transform.gameObject;
            gameManager.SelectObject(selectedObject, mouseButton);
        }

    }

    private void DeselectObject(GameObject obj)
    {
        gameManager.DeselectObject(obj);
    }

    private void ChangeCameraControl()
    {
        cameraController.GetComponent<CameraMouseMovementController>().enabled = 
            !cameraController.GetComponent<CameraMouseMovementController>().enabled;

        bool isEnabled = cameraController.GetComponent<CameraMouseMovementController>().enabled;
     
        if (isEnabled)
            mouseCameraControlButtonTxt.text = "Mouse + Keyboard\nCamera Control";
        else
            mouseCameraControlButtonTxt.text = "Keyboard Camera Control";
    }
}
