using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;
    public EnemyBehaviour enemyPrefab;
    public List<EnemyBehaviour> enemies;

    GameObject enemyParent;

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
        enemyParent = new GameObject("ENEMIES");

        // Spawn logic currently in EnemySpawnPointBehaviour.cs
        //if (enemyPrefab != null)
        //{
        //    for (int i = 0; i < enemyAmount; i++)
        //    {
        //        SpawnEnemy();
        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Spawn logic currently in EnemySpawnPointBehaviour.cs
        //dt += Time.deltaTime;

        //if (dt > waveTimer)
        //{
        //    SpawnEnemy();
        //    dt = 0f;
        //}

        //CheckSpawners();
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

    private Vector3 SetSpawnPosition(EnemyBehaviour e)
    {
        return e.GetSpawnPointPosition();
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
                        //enemies[i].Spawn(SetSpawnPosition());
                        enemies[i].Spawn(enemies[i].GetSpawnPointPosition());
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

        enemy.Init(this, null);

        enemy.transform.position = new Vector3(Random.Range(0f, 50f), 1f, Random.Range(0f, 50f));
        enemy.transform.SetParent(this.transform);
        enemies.Add(enemy);
    }

    public EnemyBehaviour SpawnEnemy(EnemySpawnPointBehaviour sp)
    {
        //EnemyBehaviour enemy = Instantiate(enemyPrefab, sp.transform);
        EnemyBehaviour enemy = Instantiate(enemyPrefab);
        enemy.transform.SetParent(enemyParent.transform);
        enemy.transform.position = sp.transform.position;
        enemy.Init(this, sp);
        enemies.Add(enemy);

        return enemy;
    }

    public void GetEnemy(EnemySpawnPointBehaviour sp)
    {

    }

    public List<EnemyBehaviour> GetEnemyList()
    {
        return enemies;
    }

    public float GetTimeLeftUntilNextEnemySpawn()
    {
        return waveTimer - dt;
    }

    public void DeactivateEnemies(List<EnemyBehaviour> e)
    {
        if (enemies != null)
        {
            for (int i = 0; i < e.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (e[i] == enemies[j])
                    {
                        enemies[j].gameObject.SetActive(false);
                    }
                }
            }
        }        
    }
}
