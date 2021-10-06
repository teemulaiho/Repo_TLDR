using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemySpawnPointBehaviour spawnPoint;

    NavMeshAgent agent;

    List<GameObject> targets = new List<GameObject>();
    GameObject target;
    Vector3 noTargetPos;    //If no target, walk around.

    HealthbarBehaviour healthBar;
    ReactionBehaviour reactionBar;
    
    float enemySpeed = 1f;
    int enemyMaxHealth = 10;
    int enemyHealth = 10;
    int enemyDamage = 1;
    bool isDead = false;

    float senseDT = 0;
    float senseTime = 4f;

    public bool inSpawnQueue = false;

    public void Init(EnemyManager p_enemyManager, EnemySpawnPointBehaviour p_spawnPoint)
    {
        enemyManager = p_enemyManager;
        spawnPoint = p_spawnPoint;
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthbarBehaviour>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(enemyMaxHealth);
        }

        reactionBar = GetComponentInChildren<ReactionBehaviour>();

        if (reactionBar)
        {
            reactionBar.Initialize(this);
        }

        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        if (!isDead)
        { 
            Sense();
            CheckReaction();
            Move();
        }
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

            //CheckMultipleTargets();
            CheckSingleTarget();
        }
        else if (Vector3.Distance(transform.position, noTargetPos) > 5f ||
                    Vector3.Distance(transform.position, noTargetPos) < 0.25f)
        {
            noTargetPos = new Vector3(Random.Range(transform.position.x - 5f, transform.position.x + 5f), transform.position.y, Random.Range(transform.position.z - 5f, transform.position.z + 5f));
        }
    }

    private void CheckMultipleTargets()
    {
        if (GameObject.FindGameObjectWithTag("Base") != null)
        {
            targets.Clear();
            targets.AddRange(GameObject.FindGameObjectsWithTag("Base"));
        }

        if (targets != null && targets.Count > 0)
        {
            float minDist = float.MaxValue;

            for (int i = 0; i < targets.Count; i++)
            {
                // Don't check disabled gameobjects.
                if (targets[i].gameObject.activeSelf)
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

    private void CheckSingleTarget()
    {
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Base") != null)
            {
                target = GameObject.FindGameObjectWithTag("Base");
            }
        }
        
        if (target != null && 
            !target.activeSelf)
        {
            target = null;
        }
    }

    private void Die()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        this.GetComponent<EnemyBehaviour>().enabled = false;
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
        if (target != null &&
            target.gameObject.activeSelf)
        {
            if (agent.destination != target.transform.position)
                agent.SetDestination(target.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            if (agent.destination != noTargetPos)
                agent.SetDestination(noTargetPos);
            //transform.position = Vector3.MoveTowards(transform.position, noTargetPos, enemySpeed * Time.deltaTime);
        }
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
        else if (other.CompareTag("Base"))
        {
            enemyHealth = 0;
        }
    }

    private void CheckReaction()
    {
        if (target != null && 
            target.gameObject.activeSelf)
        {
            reactionBar.SetReactionSprite(1);
        }
        else
            reactionBar.SetReactionSprite(0);
    }

    public bool InSpawnQueue()
    {
        return inSpawnQueue;
    }

    public Vector3 GetSpawnPointPosition()
    {
        return spawnPoint.transform.position;
    }

    public int GetEnemyDamage()
    {
        return enemyDamage;
    }
}
