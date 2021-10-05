using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    PlayerManager.PlayerStructures turretType;

    PlayerManager playerManager;
    BulletManager bulletManager;
    BulletManager bulletManagerPrefab;
    List<EnemyBehaviour> enemies;

    [SerializeField] LayerMask layerMask;

    public BulletBehaviour bulletPrefab;
    public List<BulletBehaviour> bullets;
    public List<ExplosionBehaviour> explosions;
    public ExplosionBehaviour explosionPrefab;

    bool beingPlaced = false;
    bool selected = false;

    GameObject castleRangeIndicator;
    GameObject selectedIndicator;

    float shootDT;
    float shootCooldown = 0.5f;

    int castleRange = 20;

    public void Initialize(PlayerManager pm, bool placing)
    {
        playerManager = pm;

        beingPlaced = placing;

        castleRangeIndicator = transform.Find("RangeIndicator").gameObject;
        ResizeRangeAreaIndicator();

        selectedIndicator = transform.Find("SelectionIndicator").gameObject;

        bulletManager = Instantiate(bulletManagerPrefab);
        bulletManager.transform.SetParent(this.transform);
        bulletManager.name = "BulletManager";
        bulletManager.Initialize(playerManager, this);
    }

    private void Awake()
    {
        bulletManagerPrefab = Resources.Load<BulletManager>("Prefabs/BulletManager");
        bulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullet");
        explosionPrefab = Resources.Load<ExplosionBehaviour>("Prefabs/ExplosionCannon");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetEnemies();
        SpawnBullet();
        SpawnExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

        if (selected)
        {
            selectedIndicator.SetActive(selected);
        }
        else
            selectedIndicator.SetActive(false);

        if (!beingPlaced)
        {
            ScanEnvironment();
            Act();
        }
        else
        {
            WaitForPlacement();
        }
    }

    private void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (selected)
                MoveCastle(true);
        }
    }

    private void WaitForPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerManager.RemoveObject(this.gameObject);
            Destroy(this.gameObject);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlaceCastle();
        }
    }

    private void PlaceCastle()
    {
        beingPlaced = false;
        this.tag = "Castle";
    }

    //void SpawnBullets()
    //{
    //    if (enemies.Count > 0)
    //    {
    //        for (int i = 0; i < enemies.Count; i++)
    //        {
    //            BulletBehaviour bullet = Instantiate(bulletPrefab, this.transform);
    //            bullet.name = "Bullet";

    //            bullet.gameObject.SetActive(false);

    //            bullet.Initialize(this);
    //            bullets.Add(bullet);
    //        }
    //    }
    //    else
    //    {
    //        GetEnemies();
    //    }
    //}

    private void SpawnBullet()
    {
        if (turretType == PlayerManager.PlayerStructures.TowerDirect)
        {
            bulletManager.SpawnBulletType(BulletManager.BulletType.Direct);
        }
        else if (turretType == PlayerManager.PlayerStructures.TowerCone)
        {
            bulletManager.SpawnBulletType(BulletManager.BulletType.Cone);
        }        
        else if (turretType == PlayerManager.PlayerStructures.TowerAOE)
        {
            bulletManager.SpawnBulletType(BulletManager.BulletType.AOE);
        }


        //BulletBehaviour bullet = Instantiate(bulletPrefab);
        //bullet.transform.position = this.transform.position;

        //bullet.transform.SetParent(this.transform);
        //bullet.name = "Bullet";

        //bullet.gameObject.SetActive(false);

        //bullet.Initialize(this);

        //Vector4 upgradeLevels = new Vector4();
        //upgradeLevels = playerManager.GetUpgradeLevel();

        //bullet.SetNewBulletStats((int)upgradeLevels.x, (int)upgradeLevels.y);

        //bullets.Add(bullet);
    }

    private void SpawnExplosion()
    {
        ExplosionBehaviour exp = Instantiate(explosionPrefab, this.transform);
        exp.name = "Explosion";
        explosions.Add(exp);
    }

    private void ScanEnvironment()
    {

    }

    private void GetEnemies()
    {
        enemies = playerManager.GetEnemies();
    }

    private void Act()
    {
        Shoot();
    }

    private void Shoot()
    {
        bulletManager.Shoot(enemies);
    }

    private void ShootAvailableBullet(GameObject target)
    {
        //shootDT += Time.deltaTime;

        //if (shootDT >= shootCooldown)
        //{
        //    for (int i = 0; i < bullets.Count; i++)
        //    {
        //        bool isActive = bullets[i].gameObject.activeSelf;

        //        if (!isActive)
        //        {
        //            bullets[i].gameObject.SetActive(!isActive);
        //            bullets[i].SetTarget(target);

        //            Explode();

        //            break;
        //        }     
        //    }

        //    shootDT = 0f;
        //}
    }

    public void Explode()
    {
        if (explosions.Count > 0)
        {
            explosions[0].Explode(transform.position);
        }
    }

    private void ResizeRangeAreaIndicator()
    {
        //Vector3 curSize = castleRangeIndicator.transform.localScale;
        //curSize = new Vector3(castleRange * 2, curSize.y, castleRange * 2);
        //castleRangeIndicator.transform.localScale = curSize;

        //Vector3 curSize = castleRangeIndicator.transform.lossyScale;
        Vector3 curSize = new Vector3(castleRange * (1 - 1 / transform.localScale.x), castleRangeIndicator.transform.localScale.y, castleRange * (1 - 1 / transform.localScale.x));
        castleRangeIndicator.transform.localScale = curSize;
    }

    private void MoveCastle(bool isMovable)
    {
        if (isMovable)
        {
            beingPlaced = true;
        }
    }

    public void AddExperience()
    {
        playerManager.AddExperience();
    }

    public void IncreaseStrength()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].SetDamage();
        }
    }

    public void IncreaseSpeed()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].SetSpeed();
        }
    }

    public void IncreaseRange()
    {
        castleRange++;
        ResizeRangeAreaIndicator();
    }

    public void IncreaseAmmo(BulletManager.BulletType type)
    {
        //SpawnBullet();
        bulletManager.SpawnBullet(type);
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
    }

    public int GetBulletStrength()
    {
        if (bulletManager)
            return bulletManager.GetBulletStrength();

        return -1;
    }

    public int GetBulletSpeed()
    {
        if (bulletManager)
            return bulletManager.GetBulletSpeed();

        return -1;
    }

    public int GetCastleRange()
    {
        return castleRange;
    }

    public int GetCastleAmmo()
    {
        return bullets.Count;
    }

    public float GetCastleShootDT()
    {
        return shootDT;
    }

    public void AddCastleShootDT(float dt)
    {
        shootDT += dt;
    }

    public void SetCastleShootDT(float value)
    {
        shootDT = value;
    }

    public float GetShootCooldown()
    {
        return shootCooldown;
    }

    public void SetTowerBulletType(BulletManager.BulletType type)
    {
        bulletManager.SetBulletType(type);
    }

    public void SelectCastle(int mouseButton)
    {
        selected = true;      
    }

    public void DeselectCastle()
    {
        selected = false;
    }

    public UpgradeManager GetUpgradeManager()
    {
        return playerManager.GetUpgradeManager();
    }

    public void SetTowerType(PlayerManager.PlayerStructures type)
    {
        turretType = type;
    }
}
