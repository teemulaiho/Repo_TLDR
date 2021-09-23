using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointBehaviour : MonoBehaviour
{
    SpawnManager spawnManager;
    EnemyManager enemyManager;

    [SerializeField] float spawnPointDT = 0f;
    [SerializeField] float spawnPointTimer = 8f;

    public void Initialize(SpawnManager sm, EnemyManager em)
    {
        spawnManager = sm;
        enemyManager = em;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnPointDT += Time.deltaTime;

        if (spawnPointDT >= spawnPointTimer)
        {
            enemyManager.SpawnEnemy(this);
            spawnPointDT = 0f;
        }
    }

    public float GetTimer()
    {
        return spawnPointDT;
    }
}
