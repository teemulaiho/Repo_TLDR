using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Small,
        Medium,
        Big
    }

    public enum Debuff
    {
        Slow,
    }

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private Health healthScript;
    [Space, SerializeField] private EnemyType enemyType;
    [SerializeField] private float enemyAgentSpeed = 3f;
    [SerializeField] private float enemySpeedMultiplier = 1f;
    [Space, SerializeField] private Color originalColor;
    [SerializeField] private List<Debuff> debuffList;
    

    private float enemyDamage;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void OnEnable()
    {
        waveManager.AddEnemyToList(this);
    }

    private void Start()
    {
        debuffList = new List<Debuff>();

        enemyAgent.SetDestination(new Vector3(0, 0, 0));
        enemyAgent.speed = enemyAgentSpeed;

        originalColor = GetComponentInChildren<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Health baseHealth = other.transform.GetComponent<Health>();

            if (baseHealth != null)
            {
                baseHealth.TakeDamage(enemyDamage);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.transform.GetComponent<Bullet>();

            if (bullet != null)
            {
                float damage = bullet.GetDamage();

                healthScript.TakeDamage(damage);
            }

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        healthScript.TakeDamage(damage);
    }

    public void DebuffSlowDown(float slowTimer, float speedMultiplier)
    {
        enemySpeedMultiplier = speedMultiplier;

        if (!debuffList.Contains(Debuff.Slow))
            StartCoroutine("AddDebuff", slowTimer);
    }

    private IEnumerator AddDebuff(float slowTimer)
    {
        debuffList.Add(Debuff.Slow);
        float newSpeed = enemyAgentSpeed * enemySpeedMultiplier;
        enemyAgent.speed = newSpeed;

        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        mr.material.color = Color.blue;

        yield return new WaitForSeconds(slowTimer);

        enemyAgent.speed = enemyAgentSpeed;
        mr.material.color = originalColor;
        debuffList.Remove(Debuff.Slow);
    }

    private void OnDestroy()
    {
        waveManager.RemoveEnemyFromList(this);
    }
}
