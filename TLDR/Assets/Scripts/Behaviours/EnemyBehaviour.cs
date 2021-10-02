using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemySpawnPointBehaviour spawnPoint;

    List<GameObject> targets = new List<GameObject>();
    GameObject target;
    Vector3 noTargetPos;    //If no target, walk around.

    HealthbarBehaviour healthBar;
    ReactionBehaviour reactionBar;
    
    float enemySpeed = 1f;
    int enemyMaxHealth = 10;
    int enemyHealth = 10;

    float senseDT = 0;
    float senseTime = 6f;

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
        CheckReaction();
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
            else if (Vector3.Distance(transform.position, noTargetPos) > 5f ||
                Vector3.Distance(transform.position, noTargetPos) < 0.25f)
            {
                noTargetPos = new Vector3(Random.Range(transform.position.x, transform.position.x + 5f), transform.position.y, Random.Range(transform.position.z, transform.position.z + 5f));
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
        else
            transform.position = Vector3.MoveTowards(transform.position, noTargetPos, enemySpeed * Time.deltaTime);
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

    private void CheckReaction()
    {
        if (target != null)
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
}
