using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public enum BulletType
    {
        Direct,
        Cone,
        AOE,
        Cannon
    }

    PlayerManager playerManager;
    CastleBehaviour castle;

    [SerializeField] BulletBehaviour bulletPrefab;

    public List<BulletBehaviour> bullets;

    public void Initialize(PlayerManager pm, CastleBehaviour cs)
    {
        playerManager = pm;
        castle = cs;
    }

    private void Awake()
    {
        bulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullet");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBulletType(BulletType type)
    {
        if (type == BulletType.Direct)
        {
            SpawnBullet(type);
        }
    }

    public void SpawnBullet(BulletType type)
    {
        BulletBehaviour bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.transform.SetParent(this.transform);
        bullet.name = "Bullet";
        bullet.gameObject.SetActive(false);
        bullet.Initialize(castle, type);
        Vector4 upgradeLevels = new Vector4();
        upgradeLevels = playerManager.GetUpgradeLevel();
        bullet.SetNewBulletStats((int)upgradeLevels.x, (int)upgradeLevels.y);
        bullets.Add(bullet);
    }

    public void Shoot(List<EnemyBehaviour> enemies)
    {
        EnemyBehaviour nearestEnemy = null;
        float dist = float.MaxValue;

        if (enemies != null && enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    float newDist = Vector3.Distance(this.transform.position, enemies[i].transform.position);

                    if (newDist < dist)
                    {
                        dist = newDist;
                        nearestEnemy = enemies[i];
                    }
                }
            }
        }

        if (dist <= castle.GetCastleRange())
        {
            if (!nearestEnemy.InSpawnQueue())
            {
                ShootAvailableBullet(nearestEnemy.gameObject);
            }
        }
    }

    private void ShootAvailableBullet(GameObject target)
    {
        castle.AddCastleShootDT(Time.deltaTime);

        if (castle.GetCastleShootDT() >= castle.GetShootCooldown())
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bool isActive = bullets[i].gameObject.activeSelf;

                if (!isActive)
                {
                    bullets[i].gameObject.SetActive(!isActive);
                    bullets[i].SetTarget(target);

                    castle.Explode();

                    break;
                }
            }

            castle.SetCastleShootDT(0f);
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
}
