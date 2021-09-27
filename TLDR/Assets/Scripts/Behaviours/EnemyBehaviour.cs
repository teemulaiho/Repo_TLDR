using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyManager enemyManager;

    List<GameObject> targets = new List<GameObject>();
    GameObject target;

    HealthbarBehaviour healthBar;
    
    float enemySpeed = 1f;
    int enemyMaxHealth = 10;
    int enemyHealth = 10;

    float senseDT = 0;
    float senseTime = 4f;

    public bool inSpawnQueue = false;

    public void Init(EnemyManager p_enemyManager)
    {
        enemyManager = p_enemyManager;
    }

    private void Awake()
    {
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
        Sense();
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

    private void Sense()
    {
        senseDT += Time.deltaTime;

        if (senseDT >= senseTime)
        {
            senseDT = 0f;

            if (GameObject.FindGameObjectWithTag("Castle") != null)
            {
                targets.Clear();
                targets.AddRange(GameObject.FindGameObjectsWithTag("Castle"));
            }

            if (targets != null && targets.Count > 0)
            {
                float minDist = float.MaxValue;

                for (int i = 0; i < targets.Count; i++)
                {                 
                    float dist = Vector3.Distance(transform.position, targets[i].transform.position);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        target = targets[i];
                    }
                }
            }
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
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
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
