using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public enum BulletType
    {
        Direct,
        Cone,
        AOE
    }

    PlayerManager playerManager;
    TurretBehaviour castle;

    [SerializeField] BulletBehaviour directBulletPrefab;
    [SerializeField] BulletBehaviour coneBulletPrefab;
    [SerializeField] BulletBehaviour aoeBulletPrefab;

    public List<BulletBehaviour> bullets;

    public void Initialize(PlayerManager pm, TurretBehaviour cs)
    {
        playerManager = pm;
        castle = cs;
    }

    private void Awake()
    {
        directBulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullets/DirectBullet");
        coneBulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullets/ConeBullet");
        aoeBulletPrefab = Resources.Load<BulletBehaviour>("Prefabs/Bullets/AOEBullet");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBulletTypeAttributes(BulletBehaviour bullet, BulletType type)
    {
        bullet.transform.position = this.transform.position;
        bullet.transform.SetParent(this.transform);
        bullet.name = "Bullet";
        bullet.gameObject.SetActive(false);
        bullet.Initialize(castle, type);
        //Vector4 upgradeLevels = new Vector4();
        //upgradeLevels = playerManager.GetUpgradeLevel();
        //bullet.SetNewBulletStats((int)upgradeLevels.x, (int)upgradeLevels.y);

        if (type == BulletType.Direct)
        {
            bullet.SetNewBulletStats(2, 4);
        }
        else if (type == BulletType.Cone)
        {
            bullet.SetNewBulletStats(6, 2);
        }
        else if (type == BulletType.AOE)
        {
            bullet.SetNewBulletStats(10, 1);
        }

        bullets.Add(bullet);
    }

    public void SpawnBulletType(BulletType type)
    {
        SpawnBullet(type);
    }

    public void SpawnBullet(BulletType type)
    {
        if (type == BulletType.Direct)
        {
            BulletBehaviour bullet = new BulletBehaviour();
            bullet = Instantiate(directBulletPrefab);
            SetBulletTypeAttributes(bullet, type);
        }
        else if (type == BulletType.Cone)
        {
            BulletBehaviour bullet = new BulletBehaviour();
            bullet = Instantiate(coneBulletPrefab);
            SetBulletTypeAttributes(bullet, type);
        } 
        else if (type == BulletType.AOE)
        {
            BulletBehaviour bullet = new BulletBehaviour();
            bullet = Instantiate(aoeBulletPrefab);
            SetBulletTypeAttributes(bullet, type);
        }
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

    private void ClearBulletsFromList()
    {
        foreach (BulletBehaviour bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

        bullets.Clear();
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

    public void SetBulletType(BulletType type)
    {
        ClearBulletsFromList();
        bullets.Clear();
        SpawnBullet(type);
    }
}
