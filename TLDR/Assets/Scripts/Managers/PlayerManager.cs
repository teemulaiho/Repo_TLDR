using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerStructures
    {
        Base,
        Castle,
        TowerDirect,
        TowerCone,
        TowerAOE
    }

    GameManager gameManager;
    public TurretBehaviour castle;
    public TurretBehaviour castlePrefab;
    public List<TurretBehaviour> structures;

    BaseBehaviour basePrefab;
    BaseBehaviour baseObj;

    TurretBehaviour turretDirectPrefab;
    TurretBehaviour turretConePrefab;
    TurretBehaviour turretAOEPrefab;

    int experience =- 0;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;

        if (basePrefab == null)
            basePrefab = Resources.Load<BaseBehaviour>("Prefabs/Base");

        if (turretDirectPrefab == null)
            turretDirectPrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretDirect");

        if (turretConePrefab == null)
            turretConePrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretCone");

        if (turretAOEPrefab == null)
            turretAOEPrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretAOE");

        baseObj = Instantiate(basePrefab);
        baseObj.name = "Base";
        baseObj.transform.position = new Vector3(0f, 1f, 0f); 
    }

    private void Awake()
    {
        //castle = Resources.Load<CastleBehaviour>("Prefabs/Castle");

        if (basePrefab)
            basePrefab = Resources.Load<BaseBehaviour>("Prefabs/Base");

        if (turretDirectPrefab == null)
            turretDirectPrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretDirect");

        if (turretConePrefab == null)
            turretConePrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretCone");

        if (turretAOEPrefab == null)
            turretAOEPrefab = Resources.Load<TurretBehaviour>("Prefabs/Turrets/TurretAOE");

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
        if (structures != null && structures.Count > 0)
            return structures[0].GetBulletStrength();
        else return -1;

        //if (castle != null)
        //    return castle.GetBulletStrength();

    }

    public int GetPlayerSpeed()
    {
        if (structures != null && structures.Count > 0)
            return structures[0].GetBulletSpeed();
        else return -1;

        //if (castle != null)
        //    return castle.GetBulletSpeed();
    }

    public int GetCastleRange()
    {
        if (structures != null && structures.Count > 0)
            return structures[0].GetCastleRange();
        else return -1;

        //if (castle != null)
        //    return castle.GetCastleRange();
    }

    public int GetCastleAmmo()
    {
        if (structures != null && structures.Count > 0)
            return structures[0].GetCastleAmmo();
        else return -1;

        //if (castle != null)
        //    return castle.GetCastleAmmo();
    }

    public void IncreaseStrength()
    {
        for (int i = 0; i < structures.Count; i++)
        {
            structures[i].IncreaseStrength();
        }

        //castle.IncreaseStrength();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Strength);
    }

    public void IncreaseSpeed()
    {
        for (int i = 0; i < structures.Count; i++)
        {
            structures[i].IncreaseSpeed();
        }

        //castle.IncreaseSpeed();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Speed);
    }

    public void IncreaseRange()
    {
        for (int i = 0; i < structures.Count; i++)
        {
            structures[i].IncreaseRange();
        }

        //castle.IncreaseRange();
        experience -= gameManager.GetUpgradeCost(UpgradeManager.UpgradeType.Range);
    }

    public void IncreaseAmmo(BulletManager.BulletType type)
    {
        for (int i = 0; i < structures.Count; i++)
        {
            structures[i].IncreaseAmmo(type);
        }

        //castle.IncreaseAmmo();
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
            IncreaseAmmo(BulletManager.BulletType.Direct);
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
            TurretBehaviour c = Instantiate(castlePrefab);
            c.Initialize(this, true);
            c.name = "Castle";
            c.transform.position = new Vector3(Random.Range(0f, 50f), 0f, Random.Range(0f, 50f));
            c.transform.SetParent(this.transform);
            structures.Add(c);
        }
        else if (structure == PlayerStructures.TowerDirect)
        {
            TurretBehaviour t = Instantiate(turretDirectPrefab);
            t.Initialize(this, true);
            t.name = "Castle";
            t.transform.position = new Vector3(Random.Range(0f, 50f), 0f, Random.Range(0f, 50f));
            t.transform.SetParent(this.transform);
            t.SetTowerType(structure);
            structures.Add(t);
        }
        else if (structure == PlayerStructures.TowerCone)
        {
            TurretBehaviour t = Instantiate(turretConePrefab);
            t.Initialize(this, true);
            t.name = "Castle";
            t.transform.position = new Vector3(Random.Range(0f, 50f), 0f, Random.Range(0f, 50f));
            t.transform.SetParent(this.transform);
            t.SetTowerType(structure);
            structures.Add(t);
        }
        else if (structure == PlayerStructures.TowerAOE)
        {
            TurretBehaviour t = Instantiate(turretAOEPrefab);
            t.Initialize(this, true);
            t.name = "Castle";
            t.transform.position = new Vector3(Random.Range(0f, 50f), 0f, Random.Range(0f, 50f));
            t.transform.SetParent(this.transform);
            t.SetTowerType(structure);
            structures.Add(t);
        }
    }

    public void SetTowerBulletType(BulletManager.BulletType type, GameObject tower)
    {
        TurretBehaviour c = tower.GetComponent<TurretBehaviour>();

        if (c != null && 
            structures != null && 
            structures.Count > 0)
        {
            for (int i = 0; i < structures.Count; i++)
            {
                if (structures[i] == c)
                {
                    structures[i].SetTowerBulletType(type);
                }
            }
        }
        else
            Debug.Log("No castles in list. PlayerManager.cs - SetTowerBulletType()");
    }

    public void SelectObject(GameObject obj, int mouseButton)
    {
        if (structures != null && structures.Count > 0)
        {
            foreach (TurretBehaviour c in structures)
            {
                if (obj == c.gameObject)
                {
                    c.SelectCastle(mouseButton);
                }
            }
        }
    }

    public void DeselectObject(GameObject obj)
    {
        if (obj != null)
        {
            if (structures != null && structures.Count > 0)
            {
                foreach (TurretBehaviour c in structures)
                {
                    if (c != null)
                    {
                        if (obj == c.gameObject)
                        {
                            c.DeselectCastle();
                        }
                    }
                }
            }
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (obj.GetComponent<TurretBehaviour>())
        {
            if (structures.Count > 0)
            {
                gameManager.RemoveObject(obj);
                structures.Remove(obj.GetComponent<TurretBehaviour>());
            }           
        }
    }

    public UpgradeManager GetUpgradeManager()
    {
        return gameManager.GetUpgradeManager();
    }
}
