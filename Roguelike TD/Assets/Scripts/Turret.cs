using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum Buff
    {
        ShootSpeed,
    }

    [SerializeField] private string turretName = "name here";
    [SerializeField, TextArea] private string turretDescription = "description here";

    [Header("Turret Stats")]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float range = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Bullet Stats")]
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private float bulletSpeed = 5f;

    [Header("Buffs")]
    [SerializeField] private List<Buff> buffList;
    [SerializeField] private float attackSpeedMultiplier = 1f;

    private float attackCountdown;
    private float distanceToEnemy;

    public bool placeable;
    
    private Transform target;

    public event Action<Transform> ShootNow;

    public string GetTurretName() { return turretName; }
    public string GetTurretDescription() { return turretDescription; }
    
    public float GetBulletDamage() { return bulletDamage; }
    public float GetBulletSpeed() { return bulletSpeed; }

    private void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.5f);

        buffList = new List<Buff>();
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
        Vector3 dir = (target.position + target.forward * 1.5f) - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private bool AmILookingAtEnemy()
    {
        Vector3 dir = ((target.transform.position) - transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);

        if (dot > 0.9975 && distanceToEnemy >= 17)
            return true;
        else if (dot > 0.96 && distanceToEnemy <= 17 && distanceToEnemy >= 8)
            return true;
        else if (dot > 0.88 && distanceToEnemy < 8)
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
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
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

    public void AddBuffIncreaseAttackSpeed(Buff buff, float duration, float speedMultiplier)
    {
        attackSpeedMultiplier = speedMultiplier;

        if (!buffList.Contains(buff))
        {
            StartCoroutine("AddBuff", duration);
        }
    }

    private IEnumerator AddBuff(float duration)
    {
        float originalAttackSpeed = attackSpeed;
        attackSpeed = originalAttackSpeed * attackSpeedMultiplier * 10;
        buffList.Add(Buff.ShootSpeed);

        yield return new WaitForSeconds(duration);

        attackSpeed = originalAttackSpeed;
        buffList.Remove(Buff.ShootSpeed);
    }


    private void OnDrawGizmosSelected() // Turret range indicator (only inspector)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
