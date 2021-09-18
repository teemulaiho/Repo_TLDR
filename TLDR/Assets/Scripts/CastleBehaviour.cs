using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour
{
    PlayerManager playerManager;

    public List<GameObject> enemies;

    public BulletBehaviour bulletPrefab;
    public List<BulletBehaviour> bullets;

    public float castleRange;

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
        SpawnBullets();
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
                bullet.SetTarget(enemies[i]);
                

                bullets.Add(bullet);
            }
        }
        else
        {
            GetEnemies();
        }
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
                if (bullets.Count > 0)
                {
                    bool isActive = bullets[i].gameObject.activeSelf;

                    if (!isActive)
                    {
                        bullets[i].gameObject.SetActive(!isActive);
                    }
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

    public int GetBulletStrength()
    {
        if (bullets.Count > 0)
            return bullets[0].GetBulletDamage();

        return -1;
    }
}
