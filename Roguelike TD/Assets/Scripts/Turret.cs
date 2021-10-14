using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private string turretName = "name here";
    [SerializeField, TextArea] private string turretDescription = "description here";

    [Header("Turret Stats")]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float range = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Bullet Stats")]
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private float bulletSpeed = 5f;

    private float attackCountdown;
    
    private Transform target;

    public event Action<Transform> ShootNow;

    public string GetTurretName() { return turretName; }
    public string GetTurretDescription() { return turretDescription; }
    
    public float GetBulletDamage() { return bulletDamage; }
    public float GetBulletSpeed() { return bulletSpeed; }

    private void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
            return;

        AttackCountdown();
        TurretRotation();
        AmILookingAtEnemy();
    }

    private void AttackCountdown()
    {
        if (attackCountdown <= 0f && AmILookingAtEnemy())
        {
            Shoot();
            attackCountdown = 1f / attackSpeed;
        }
        attackCountdown -= Time.deltaTime;
    }

    private void TurretRotation()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private bool AmILookingAtEnemy()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);

        if (dot > 0.9)
            return true;
        else
            return false;
    }

    private void Shoot()
    {
        if (ShootNow != null)
        {
            ShootNow(target);
        }     
    }

    private void FindTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Store somewhere instead, eg GameManager 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected() // Turret range indicator (only inspector)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
