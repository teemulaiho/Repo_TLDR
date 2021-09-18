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
        return 0;
    }

    public int GetStrengthUpgradeCost()
    {
        return upgradeManager.GetStrengthUpgradeCost();
    }

    public void UpgradeStrength()
    {
        playerManager.IncreaseStrength();
        upgradeManager.IncreaseStrengthLevel();
    }
}
