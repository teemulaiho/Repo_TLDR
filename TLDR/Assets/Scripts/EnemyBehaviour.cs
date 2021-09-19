using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyManager enemyManager;
    public GameObject castle;

    HealthbarBehaviour healthBar;
    
    float enemySpeed = 1f;
    int enemyMaxHealth = 10;
    int enemyHealth = 10;

    public bool inSpawnQueue = false;

    public void Init(EnemyManager p_enemyManager)
    {
        enemyManager = p_enemyManager;
    }

    private void Awake()
    {
        castle = GameObject.FindGameObjectWithTag("Castle");

        healthBar = GetComponentInChildren<HealthbarBehaviour>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(enemyMaxHealth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        Move();
   }

    private void CheckHealth()
    {
        UpdateHealthBar();

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
        enemyManager.EnemyHasDied(this);
        transform.position = new Vector3(999f, 999f, 999f);
        inSpawnQueue = true;
    }

    private void UpdateHealthBar()
    {
        healthBar.SetHealth(enemyHealth);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, castle.transform.position, enemySpeed * Time.deltaTime);
    }

    public void Spawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        inSpawnQueue = false;
        enemyHealth = enemyMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletBehaviour bullet = other.GetComponent<BulletBehaviour>();

            enemyHealth -= bullet.GetBulletDamage();
        }
    }

    public bool InSpawnQueue()
    {
        return inSpawnQueue;
    }
}
