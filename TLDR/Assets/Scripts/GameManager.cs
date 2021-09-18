using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public UIManager uiManager;
    public UpgradeManager upgradeManager;

    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Initialize()
    {
        if (playerManager != null)
            playerManager.Initialize(this);

        if (playerManager != null)
            uiManager.Initialize(this);

        if (playerManager != null)
            enemyManager.Initialize(this);

        if (upgradeManager != null)
            upgradeManager.Initialize(this);
    }

    public int GetPlayerExperience()
    {
        return playerManager.GetPlayerExperience();
    }

    public int GetPlayerStrength()
    {
        return playerManager.GetPlayerStrength();
    }

    public int GetPlayerSpeed()
    {
        return playerManager.GetPlayerSpeed();
    }

    public int GetCastleRange()
    {
        return playerManager.GetCastleRange();
    }

    public int GetCastleAmmo()
    {
        return playerManager.GetCastleAmmo();
    }

    public int GetUpgradeCost(UpgradeManager.UpgradeType type)
    {
        return upgradeManager.GetUpgradeCost(type);
    }

    public void Upgrade(UpgradeManager.UpgradeType type)
    {
        playerManager.Upgrade(type);
        upgradeManager.IncreaseLevel(type);
    }
}
