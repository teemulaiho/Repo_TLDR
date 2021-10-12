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
    DebugManager debugManager;

    int strengthLevel = 0;
    int speedLevel = 0;
    int rangeLevel = 0;
    int ammoLevel = 0;
    int towerCount = 0;

    public int StrengthLevel
    {
        get { return strengthLevel; }
        set
        {
            if (strengthLevel == value) return;
            strengthLevel = value;
            if (OnVariableChange != null)
                OnVariableChange(strengthLevel, UpgradeType.Strength);
        }
    }

    public int SpeedLevel
    {
        get { return speedLevel; }
        set
        {
            if (speedLevel == value) return;
            speedLevel = value;
            if (OnVariableChange != null)
                OnVariableChange(speedLevel, UpgradeType.Speed);
        }
    }

    int strengthUpgradeCost = 10;
    int speedUpgradeCost = 15;
    int rangeUpgradeCost = 20;
    int ammoUpgradeCost = 30;

    int newTowerCost = 45;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;

        if (gameManager != null)
            debugManager = gameManager.GetDebugManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void OnVariableChangeDelegate(int newVal, UpgradeManager.UpgradeType type);

    public event OnVariableChangeDelegate OnVariableChange;


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

    private void DecreaseUpgradeCost(UpgradeType type)
    {
        if (type == UpgradeType.Strength)
        {
            strengthUpgradeCost--;
        }
        else if (type == UpgradeType.Speed)
        {
            speedUpgradeCost--;
        }
        else if (type == UpgradeType.Range)
        {
            rangeUpgradeCost--;
        }
        else if (type == UpgradeType.Ammo)
        {
            ammoUpgradeCost--;
        }
        else if (type == UpgradeType.NewTower)
        {
            if (towerCount == 0)
                newTowerCost = 0;
            else if (towerCount == 1)
                newTowerCost = 45;
            else
                newTowerCost = (int)((float)newTowerCost * 1.25f);
        }
    }

    /// <summary>
    /// Returns Vector4(strengthLevel, speedLevel, rangeLevel, ammoLevel);
    /// </summary>
    /// <returns></returns>
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
            //strengthLevel++;
            StrengthLevel = strengthLevel + 1;
        }
        else if (type == UpgradeType.Speed)
        {
            //speedLevel++;
            SpeedLevel = speedLevel + 1;
        }
        else if (type == UpgradeType.Range)
        {
            rangeLevel++;
        }
        else if (type == UpgradeType.Ammo)
        {
            ammoLevel++;
        }
        else if (type == UpgradeType.NewTower)
        {
            towerCount++;
        }

        IncreaseUpgradeCost(type);
    }

    public void DecreaseLevel(UpgradeType type)
    {
        if (type == UpgradeType.Strength)
        {
            strengthLevel--;
        }
        else if (type == UpgradeType.Speed)
        {
            speedLevel--;
        }
        else if (type == UpgradeType.Range)
        {
            rangeLevel--;
        }
        else if (type == UpgradeType.Ammo)
        {
            ammoLevel--;
        }
        else if (type == UpgradeType.NewTower)
        {
            towerCount--;
        }

        DecreaseUpgradeCost(type);
    }

    public DebugManager GetDebugManager()
    {
        return debugManager;
    }
}
