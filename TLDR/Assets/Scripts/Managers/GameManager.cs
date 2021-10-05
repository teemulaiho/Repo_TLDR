using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public UIManager uiManager;
    public UpgradeManager upgradeManager;
    public SpawnManager spawnManager;
    public TimerManager timerManager;

    float elapsedTime = 0f;

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
        elapsedTime += Time.deltaTime;
    }
    
    private void Initialize()
    {
        if (playerManager != null)
            playerManager.Initialize(this);

        if (uiManager != null)
            uiManager.Initialize(this);

        if (enemyManager != null)
            enemyManager.Initialize(this);

        if (upgradeManager != null)
            upgradeManager.Initialize(this);

        if (spawnManager != null)
            spawnManager.Initialize(this, enemyManager);

        if (timerManager != null)
            timerManager.Initialize(this);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public float GetTimeLeftUntilNextEnemySpawn()
    {
        return enemyManager.GetTimeLeftUntilNextEnemySpawn();
    }

    public float GetTimeLeftUntilNextEnemySpawnPoint()
    {
        return spawnManager.GetTimeLeftUntilNextEnemySpawnPoint();
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

    public int GetNewTowerPurchasePrice()
    {
        return upgradeManager.GetUpgradeCost(UpgradeManager.UpgradeType.NewTower);
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

    public List<EnemyBehaviour> GetEnemies()
    {
        return enemyManager.GetEnemyList();
    }

    public Vector4 GetUpgradeLevel()
    {
        return upgradeManager.GetUpgradeLevel();
    }

    public void Spawn(PlayerManager.PlayerStructures structure)
    {
        playerManager.Spawn(structure);
    }

    public List<float> GetTimers()
    {
        List<float> t = new List<float>();

        t.Add(spawnManager.GetSpawnManagerTimer());
        t.AddRange(spawnManager.GetSpawnPointTimers());

        return t;
    }

    public void SetTowerBulletType(BulletManager.BulletType type, GameObject tower)
    {
        playerManager.SetTowerBulletType(type, tower);
    }

    public void SelectObject(GameObject obj, int mouseButton)
    {
        if (obj.tag == "Enemy")
        {

        }
        else if (obj.tag == "EnemySpawnPoint")
        {
            spawnManager.SelectObject(obj, mouseButton);
        }
        else if (obj.tag == "Castle")
        {
            playerManager.SelectObject(obj, mouseButton);
        }        
    }

    public void DeselectObject(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {

        }
        else if (obj.tag == "Castle")
        {
            playerManager.DeselectObject(obj);
        }
        else if (obj.tag == "EnemySpawnPoint")
        {
            spawnManager.DeselectObject(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (obj.GetComponent<TurretBehaviour>())
            upgradeManager.DecreaseLevel(UpgradeManager.UpgradeType.NewTower);
    }

    public void CheckUpgrade()
    {
    }

    public TimerManager GetTimerManager()
    {
        return timerManager;
    }

    public void SetTimer(string name, float value)
    {
        if (name == "SliderSpawnPoint")
        {
            timerManager.SetSpawnPointActivationTimer(value);
        }
        else if (name == "SliderEnemySpawnRate")
        {
            timerManager.SetEnemySpawnRateTimer(value);
        }
        else if (name == "SliderEnemyHealth")
        {

        }
    }

    public UpgradeManager GetUpgradeManager()
    {
        return upgradeManager;
    }
}
