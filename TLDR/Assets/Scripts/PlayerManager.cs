using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    public CastleBehaviour castle;
    int experience = 0;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    private void Awake()
    {
        if (castle != null)
            castle.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExperience()
    {
        experience++;
    }

    public int GetPlayerExperience()
    {
        return experience;
    }

    public int GetPlayerStrength()
    {
        return castle.GetBulletStrength();
    }

    public int GetPlayerSpeed()
    {
        return castle.GetBulletSpeed();
    }

    public int GetCastleRange()
    {
        return castle.GetCastleRange();
    }

    public int GetCastleAmmo()
    {
        return castle.GetCastleAmmo();
    }

    public void IncreaseStrength()
    {
        castle.IncreaseStrength();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Strength);
    }

    public void IncreaseSpeed()
    {
        castle.IncreaseSpeed();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Speed);
    }

    public void IncreaseRange()
    {
        castle.IncreaseRange();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Range);
    }

    public void IncreaseAmmo()
    {
        castle.IncreaseAmmo();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Ammo);
    }

    public void Upgrade(UpgradeManager.UpgradeType type)
    {
        if (type == UpgradeManager.UpgradeType.Strength)
        {
            IncreaseStrength();
        }
        else if (type == UpgradeManager.UpgradeType.Speed)
        {
            IncreaseSpeed();
        }
        else if (type == UpgradeManager.UpgradeType.Range)
        {
            IncreaseRange();
        }
        else if (type == UpgradeManager.UpgradeType.Ammo)
        {
            IncreaseAmmo();
        }
    }

    public List<EnemyBehaviour> GetEnemies()
    {
        return gameManager.GetEnemies();
    }

    public Vector4 GetUpgradeLevel()
    {
        return gameManager.GetUpgradeLevel();
    }
}
