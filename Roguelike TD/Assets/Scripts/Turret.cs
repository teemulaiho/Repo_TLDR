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
    [SerializeField] private float bulletMaxTravel = 10f;

    [Header("Buffs")]
    [SerializeField] private List<Buff> buffList;
    [SerializeField] private float attackSpeedMultiplier = 1f;

    private float attackCountdown;
    private float distanceToEnemy;

    public bool placeable;
    
    private Transform target;
    private Transform prevTarget;
    [SerializeField] private GameObject indicator;

    public event Action<Transform> ShootNow;

    public string GetTurretName() { return turretName; }
    public string GetTurretDescription() { return turretDescription; }
    
    public float GetBulletDamage() { return bulletDamage; }
    public float GetBulletSpeed() { return bulletSpeed; }
    public float GetBulletMaxTravel() { return bulletMaxTravel; }

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
        //AmILookingAtEnemy();
    }

    public void RangeIndicator(bool state)
    {
        indicator.SetActive(state);
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
        Vector3 dir = (target.position + target.forward * 0.5f) - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private bool AmILookingAtEnemy()
    {
        Vector3 dir = ((target.transform.position) - transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);

        /*if (distanceToEnemy < 4)
            return dot > 0.7;
        else if (distanceToEnemy < 7)
            return dot > 0.90;
        else if (distanceToEnemy > range)
            return dot > 0.99;
        else
            return false;*/
        
        if (prevTarget != target)
        {
            if (dot > 0.995)
            {
                prevTarget = target;
                return true;
            }   
            else
                return false;
        }
        else if (dot > 0.96)
        {
            return true; 
        }
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
