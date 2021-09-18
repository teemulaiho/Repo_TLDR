using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour
{
    PlayerManager playerManager;

    public List<GameObject> enemies;

    public BulletBehaviour bulletPrefab;
    public List<BulletBehaviour> bullets;

    int castleRange = 20;

    public void Initialize(PlayerManager pm)
    {
        playerManager = pm;
    }

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        GetEnemies();
        //SpawnBullets();
        SpawnBullet();
    }

    // Update is called once per frame
    void Update()
    {
        ScanEnvironment();
        Act();
    }

    void SpawnBullets()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                BulletBehaviour bullet = Instantiate(bulletPrefab);
                bullet.transform.position = this.transform.position;

                bullet.transform.SetParent(this.transform);
                bullet.name = "Bullet";

                bullet.gameObject.SetActive(false);

                bullet.Initialize(this);
                //bullet.SetTarget(enemies[i]);
                

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

        bullets.Add(bullet);
    }

    private void ScanEnvironment()
    {

    }

    private void GetEnemies()
    {
        if (enemies.Count == 0)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        }
    }

    private void Act()
    {
        Shoot();
    }

    private void Shoot()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(this.transform.position, enemies[i].transform.position) <= castleRange)
            {
                ShootAvailableBullet(enemies[i]);
            }
        }
    }

    private void ShootAvailableBullet(GameObject target)
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bool isActive = bullets[i].gameObject.activeSelf;

                if (!isActive)
                {
                    bullets[i].gameObject.SetActive(!isActive);
                    bullets[i].SetTarget(target);
                }
            }
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
