using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : MonoBehaviour
{
    public GameObject enemy;

    public List<GameObject> enemies;

    public BulletBehaviour bulletPrefab;
    public List<BulletBehaviour> bullets;

    public float castleRange;

    private void Awake()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        SpawnBullets();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScanEnvironment();
        Act();
    }

    void SpawnBullets()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            BulletBehaviour bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position;

            bullet.transform.SetParent(this.transform);
            bullet.name = "Bullet";

            bullet.gameObject.SetActive(false);

            bullet.SetTarget(enemies[i]);

            bullets.Add(bullet);
        }


        //if (bullets.Count < 1)
        //{
        //    BulletBehaviour bullet = Instantiate(bulletPrefab);
        //    bullet.transform.position = this.transform.position;

        //    bullet.transform.SetParent(this.transform);
        //    bullet.name = "Bullet";

        //    bullet.gameObject.SetActive(false);

        //    bullets.Add(bullet);
        //}
    }

    void ScanEnvironment()
    {

    }

    void Act()
    {
        Shoot();
    }

    void Shoot()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) <= castleRange)
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
}
