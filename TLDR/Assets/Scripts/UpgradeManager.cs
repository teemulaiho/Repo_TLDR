using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeType
    {
        Strength,
        Speed,
        Range,
        Ammo
    }

    GameManager gameManager;

    int strengthLevel = 1;
    int speedLevel = 1;
    int rangeLevel = 1;
    int ammoLevel = 1;

    int strengthUpgradeCost = 10;
    int speedUpgradeCost = 15;
    int rangeUpgradeCost = 20;
    int ammoUpgradeCost = 30;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetUpgradeCost(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.Strength)
        {
            return strengthUpgradeCost;
        }
        else if (upgradeType == UpgradeType.Speed)
        {
            return speedUpgradeCost;
        }
        else if (upgradeType == UpgradeType.Range)
        {
            return rangeUpgradeCost;
        }
        else if (upgradeType == UpgradeType.Ammo)
        {
            return ammoUpgradeCost;
        }

        return -1;
    }

    public void IncreaseLevel(UpgradeType type)
    {
        if (type == UpgradeType.Strength)
        {
            strengthLevel++;
            IncreaseUpgradeCost(type);
        }
        else if (type == UpgradeType.Speed)
        {
            speedLevel++;
            IncreaseUpgradeCost(type);
        }
        else if (type == UpgradeType.Range)
        {
            rangeLevel++;
            IncreaseUpgradeCost(type);
        }
        else if (type == UpgradeType.Ammo)
        {
            ammoLevel++;
            IncreaseUpgradeCost(type);
        }
    }

    private void IncreaseUpgradeCost(UpgradeType type)
    {
        if (type == UpgradeType.Strength)
        {
            strengthUpgradeCost++;
        }
        else if (type == UpgradeType.Speed)
        {
            speedUpgradeCost++;
        }
        else if (type == UpgradeType.Range)
        {
            rangeUpgradeCost++;
        }
        else if (type == UpgradeType.Ammo)
        {
            ammoUpgradeCost++;
        }
    }
}
