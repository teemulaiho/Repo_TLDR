using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
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

    Button strengthButton;
    Button speedButton;
    Button rangeButton;
    Button ammoButton;

    Button castleSpawnButton;

    Button changeTowerBulletTypeToDirectButton;
    Button changeTowerBulletTypeToConeButton;


    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    private void Awake()
    {
        ui = Instantiate(UIprefab);
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

        SetUI();
        CheckButtons();
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

    private void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // First deselect current object.
            if (selectedObject != null)
            {
                DeselectObject(selectedObject);
            }

            RaycastHit hitInfo = new RaycastHit();
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
        selectedObject = obj.transform.gameObject;
        gameManager.SelectObject(selectedObject, mouseButton);
    }

    private void DeselectObject(GameObject obj)
    {
        gameManager.DeselectObject(obj);
    }
}
