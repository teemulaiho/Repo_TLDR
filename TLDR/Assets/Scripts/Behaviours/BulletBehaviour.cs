using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    BulletManager.BulletType bulletType;

    CastleBehaviour castle;
    public Vector3 castlePosition;
    public GameObject target;
    public ExplosionBehaviour explosionPrefab;

    public List<ExplosionBehaviour> explosions;

    int bulletSpeed = 4;
    int bulletDamage = 2;

    public void Initialize(CastleBehaviour cb)
    {
        castle = cb;
        castlePosition = castle.transform.position;
    }

    public void Initialize(CastleBehaviour cb, BulletManager.BulletType type)
    {
        castle = cb;
        castlePosition = castle.transform.position;
        bulletType = type;

        if (bulletType == BulletManager.BulletType.Direct)
        {
            SetBulletStartingStats(2,4);
        }
        else if (bulletType == BulletManager.BulletType.Cone)
        {
            SetBulletStartingStats(6, 2);
        }
    }

    private void Awake()
    {
        transform.position = castlePosition;

        SpawnExplosion();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target.activeSelf)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
        else
        {
            ResetPosition();
            ResetTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Hit();
        }
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    public int GetBulletSpeed()
    {
        return bulletSpeed;
    }

    /// <summary>
    /// Add additional stats to the bullet base stats. Base stats are damage = 2, speed = 4.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="speed"></param>
    public void SetNewBulletStats(int damage, int speed)
    {
        bulletDamage += damage;
        bulletSpeed += speed;
    }

    private void Hit()
    {
        Explode();
        ResetPosition();
        ResetTarget();
        AddExperience();
    }

    private void SpawnExplosion()
    {
        if (explosions.Count < 1)
        {
            ExplosionBehaviour explosion = Instantiate(explosionPrefab);
            explosion.transform.position = this.transform.position;

            explosion.name = "Explosion";
            explosions.Add(explosion);
        }
    }

    public void Explode()
    {
        if (explosions.Count > 0)
        {
            explosions[0].Explode(transform.position);
        }
    }

    private void ResetPosition()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        transform.position = castle.transform.position;
    }

    private void ResetTarget()
    {
        target = null;
    }

    private void AddExperience()
    {
        castle.AddExperience();
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public void SetDamage()
    {
        bulletDamage++;
    }

    public void SetSpeed()
    {
        bulletSpeed++;
    }

    public void SetBulletStartingStats(int damage, int speed)
    {
        bulletDamage += damage;
        bulletSpeed += speed;
    }

    private void OnDestroy()
    {
        foreach (ExplosionBehaviour explosion in explosions)
        {
            Destroy(explosion.gameObject);
        }

        explosions.Clear();
    }
}
