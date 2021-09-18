using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
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
    }

    private void Awake()
    {
        castlePosition = GameObject.FindGameObjectWithTag("Castle").transform.position;
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

    private void Explode()
    {
        if (explosions.Count > 0)
        {
            explosions[0].Explode(transform.position);
        }
    }

    private void ResetPosition()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        transform.position = castlePosition;
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
}
