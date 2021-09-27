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
        Ammo,
        NewTower
    }

    GameManager gameManager;

    int strengthLevel = 0;
    int speedLevel = 0;
    int rangeLevel = 0;
    int ammoLevel = 0;
    int towerCount = 0;

    int strengthUpgradeCost = 10;
    int speedUpgradeCost = 15;
    int rangeUpgradeCost = 20;
    int ammoUpgradeCost = 30;

    int newTowerCost = 45;

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

    public Vector4 GetUpgradeLevel()
    {
        return new Vector4(strengthLevel, speedLevel, rangeLevel, ammoLevel);
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
        else if (upgradeType == UpgradeType.NewTower)
        {
            if (towerCount == 0)
                return 0;
            else
                return newTowerCost;
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
        else if (type == UpgradeType.NewTower)
        {
            towerCount++;
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
        else if (type == UpgradeType.NewTower)
        {
            if (towerCount == 1)
                newTowerCost = 45;
            else
                newTowerCost = (int)((float)newTowerCost * 1.25f);
        }
    }
}