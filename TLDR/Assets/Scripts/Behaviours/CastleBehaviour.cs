using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour
{
    PlayerManager playerManager;
    List<EnemyBehaviour> enemies;

    [SerializeField] LayerMask layerMask;

    public BulletBehaviour bulletPrefab;
    public List<BulletBehaviour> bullets;

    bool beingPlaced = false;


    GameObject castleRangeIndicator;

    float shootDT;
    float shootCooldown = 0.5f;

    int castleRange = 20;

    public void Initialize(PlayerManager pm, bool placing)
    {
        playerManager = pm;

        beingPlaced = placing;

        castleRangeIndicator = transform.Find("RangeIndicator").gameObject;
        ResizeRangeAreaIndicator();
    }

    private void Awake()
    {
        bulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullet");
    }


    // Start is called before the first frame update
    void Start()
    {
        GetEnemies();
        SpawnBullet();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void WaitForPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceCastle();
        }
    }

    private void PlaceCastle()
    {
        beingPlaced = false;
        this.tag = "Castle";
    }

    void SpawnBullets()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                BulletBehaviour bullet = Instantiate(bulletPrefab, this.transform);
                bullet.name = "Bullet";

                bullet.gameObject.SetActive(false);

                bullet.Initialize(this);
                bullets.Add(bullet);
            }
        }
        else
        {
            GetEnemies();
        }
    }

    private void SpawnBullet()
    {
        BulletBehaviour bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;

        bullet.transform.SetParent(this.transform);
        bullet.name = "Bullet";

        bullet.gameObject.SetActive(false);

        bullet.Initialize(this);

        Vector4 upgradeLevels = new Vector4();
        upgradeLevels = playerManager.GetUpgradeLevel();

        bullet.SetNewBulletStats((int)upgradeLevels.x, (int)upgradeLevels.y);

        bullets.Add(bullet);
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
        EnemyBehaviour nearestEnemy = null;
        float dist = float.MaxValue;

        for (int i = 0; i < enemies.Count; i++)
        {
            float newDist = Vector3.Distance(this.transform.position, enemies[i].transform.position);

            if (newDist < dist)
            {
                dist = newDist;
                nearestEnemy = enemies[i];
            }
        }

        if (dist <= castleRange)
        {
            if (!nearestEnemy.InSpawnQueue())
                ShootAvailableBullet(nearestEnemy.gameObject);
        }
    }

    private void ShootAvailableBullet(GameObject target)
    {
        shootDT += Time.deltaTime;

        if (shootDT >= shootCooldown)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bool isActive = bullets[i].gameObject.activeSelf;

                if (!isActive)
                {
                    bullets[i].gameObject.SetActive(!isActive);
                    bullets[i].SetTarget(target);

                    break;
                }     
            }

            shootDT = 0f;
        }
    }

    private void ResizeRangeAreaIndicator()
    {
        Vector3 curSize = castleRangeIndicator.transform.localScale;
        curSize = new Vector3(castleRange * 2, curSize.y, castleRange * 2);
        castleRangeIndicator.transform.localScale = curSize;
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

    public void IncreaseAmmo()
    {
        SpawnBullet();
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
        if (bullets.Count > 0)
            return bullets[0].GetBulletDamage();

        return -1;
    }

    public int GetBulletSpeed()
    {
        if (bullets.Count > 0)
            return bullets[0].GetBulletSpeed();

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
}
