using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletType_Explode : MonoBehaviour
{
    [SerializeField] private Bullet bulletScript;
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private SphereCollider bulletCollider;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private List<Collider> enemies;
    private float explosionRadius = 8f;

    private void Awake()
    {
        if (bulletScript == null)
            bulletScript = GetComponent<Bullet>();

        if (explosionPrefab == null)
            explosionPrefab = Resources.Load<ParticleSystem>("Prefabs/Explosion");

        if (bulletCollider == null)
            bulletCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        enemies = new List<Collider>();

        if (bulletScript != null)
        {
            bulletScript.HitNow += HitNow;
        }
    }

    public void SetExplosionRadius(float r)
    {
        explosionRadius = r;
    }

    private void HitNow(Transform transform)
    {
        enemies.AddRange(Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer));

        foreach (Collider c in enemies)
        {
            Debug.Log(enemies.Count);
            c.GetComponentInParent<Enemy>().TakeDamage(bulletScript.GetDamage());
        }

        enemies.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ParticleSystem explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
            explosion.Play();

            bulletScript.HitDestination();
        }
    }
}
