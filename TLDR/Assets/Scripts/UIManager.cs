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

    GameObject xpObj;
    TMP_Text xpTxt;

    GameObject strObj;
    TMP_Text strTxt;

    GameObject spdObj;
    TMP_Text spdTxt;

    Button strengthButton;
    Button speedButton;

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
            if (ui.transform.GetChild(i).name == "Strength")
            {
                strObj = ui.transform.GetChild(i).gameObject;
                strTxt = strObj.GetComponent<TMP_Text>();
            }
            if (ui.transform.GetChild(i).name == "Seed")
            {
                spdObj = ui.transform.GetChild(i).gameObject;
                spdTxt = spdObj.GetComponent<TMP_Text>();
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();
        CheckButtons();
    }

    private void SetUI()
    {
        xpTxt.text = gameManager.GetPlayerExperience().ToString();
        strTxt.text = gameManager.GetPlayerStrength().ToString();
        //spdTxt.text = gameManager.GetPlayerSpeed().ToString();
    }

    private void IncreaseStrength()
    {
        Debug.Log("Clicked Strength Button.");
        gameManager.UpgradeStrength();
    }

    private void IncreaseSpeed()
    {
        Debug.Log("Clicked Speed Button.");
    }

    private void CheckButtons()
    {        
        CheckStrengthUpgrade();
    }

    private void CheckStrengthUpgrade()
    {
        int playerXP = gameManager.GetPlayerExperience();

        if (playerXP >= gameManager.GetStrengthUpgradeCost())
        {
            strengthButton.interactable = true;
        }
        else
        {
            strengthButton.interactable = false;
        }
    }
}
