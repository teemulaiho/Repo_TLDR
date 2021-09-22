using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerStructures
    {
        Castle
    }

    GameManager gameManager;
    public CastleBehaviour castle;
    public CastleBehaviour castlePrefab;
    public List<CastleBehaviour> castles;

    int experience = 0;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    private void Awake()
    {
        if (castle != null)
            castle.Initialize(this, false);
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
        if (castle != null)
            return castle.GetBulletStrength();
        else return -1;
    }

    public int GetPlayerSpeed()
    {
        if (castle != null)
            return castle.GetBulletSpeed();
        else return -1;
    }

    public int GetCastleRange()
    {
        if (castle != null)
            return castle.GetCastleRange();
        else return -1;
    }

    public int GetCastleAmmo()
    {
        if (castle != null)
            return castle.GetCastleAmmo();
        else return -1;
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

    public void Spawn(PlayerStructures structure)
    {
        if (structure == PlayerStructures.Castle)
        {
            CastleBehaviour c = Instantiate(castlePrefab);
            c.Initialize(this, true);
            c.transform.position = new Vector3(Random.Range(0f, 50f), 0f, Random.Range(0f, 50f));
            c.transform.SetParent(this.transform);
            castles.Add(c);
        }
    }
}
