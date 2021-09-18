using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyBehaviour enemyPrefab;
    public List<EnemyBehaviour> enemies;

    public int enemyAmount = 1;

    bool startSpawnTimer = false;
    float spawnTimer = 0f;
    float spawnLimit = 3f;

    private void Awake()
    {
        if (enemyPrefab != null)
        {
            for (int i = 0; i < enemyAmount; i++)
            {
                EnemyBehaviour enemy = Instantiate(enemyPrefab);

                enemy.Init(this);

                enemy.transform.position = new Vector3(Random.Range(0f, 50f), 1f, Random.Range(0f, 50f));
                enemy.transform.SetParent(this.transform);
                enemies.Add(enemy);
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
}
