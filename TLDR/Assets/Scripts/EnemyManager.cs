using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;
    public EnemyBehaviour enemyPrefab;
    public List<EnemyBehaviour> enemies;

    int enemyAmount = 1;

    float dt = 0f;
    bool startSpawnTimer = false;
    float spawnTimer = 0f;
    float spawnLimit = 3f;
    int waveTimer = 60;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    private void Awake()
    {
        if (enemyPrefab != null)
        {
            for (int i = 0; i < enemyAmount; i++)
            {
                SpawnEnemy();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;

        if (dt > waveTimer)
        {
            Debug.Log("Spawn New Enemy");
            SpawnEnemy();
            dt = 0f;
        }

        CheckSpawners();
    }

    public void EnemyHasDied(EnemyBehaviour enemy)
    {
        if (!startSpawnTimer)
            startSpawnTimer = true;
    }

    private Vector3 SetSpawnPosition()
    {
        return new Vector3(Random.Range(0f, 50f), 1f, Random.Range(0f, 50f));
    }

    private void CheckSpawners()
    {
        if (startSpawnTimer)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnLimit)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].inSpawnQueue)
                    {
                        enemies[i].Spawn(SetSpawnPosition());
                        enemies[i].gameObject.SetActive(true);
                    }
                }

                spawnTimer = 0f;
                startSpawnTimer = false;
            }
        }
    }

    private void SpawnEnemy()
    {
        EnemyBehaviour enemy = Instantiate(enemyPrefab);

        enemy.Init(this);

        enemy.transform.position = new Vector3(Random.Range(0f, 50f), 1f, Random.Range(0f, 50f));
        enemy.transform.SetParent(this.transform);
        enemies.Add(enemy);
    }

    public List<EnemyBehaviour> GetEnemyList()
    {
        return enemies;
    }
}
