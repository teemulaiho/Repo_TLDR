using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private List<GameObject> enemyPrefabList;
    [SerializeField] private Transform spriteTransform;

    [SerializeField] private MeshRenderer meshRenderer;

    [Space, SerializeField] private int spawnerResource = 0;

    private bool inCombat = true;
    [Space, SerializeField] private bool active = true;

    private SpawnManager spawnManager;
    public bool willBeActivated = false;

    private Vector3 range;

    public void SetSpawnerResource(int value)
    {
        spawnerResource = value;
    }

    public void SetActiveBool(bool state)
    {
        active = state;
    }

    public void WillBeActivatedAtRoundStart(bool state)
    {
        if (state)
        {
            willBeActivated = true;
            spawnManager.AddSpawnerToList(this);
            
            // Change color?
            meshRenderer.material.color = Color.blue;
        }
        else
        {
            willBeActivated = false;
            spawnManager.RemoveSpawnerFromList(this);

            // Change back to default color
            float r = 120f / 255f;
            meshRenderer.material.color = new Color(r, 0, 0);
        }
    }

    private void Awake()
    {
        enemyParent = GameObject.Find("ENEMYPARENT").transform;
        waveManager = FindObjectOfType<WaveManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        enemyPrefabList = new List<GameObject>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/SmallEnemy"));
        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/MediumEnemy"));
        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/BigEnemy"));
    }

    private void Start()
    {
        //range = spriteTransform.localScale;
        range = transform.localScale;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (inCombat)
        {
            while (active)
            {
                float waitTime = Random.Range(1.5f, 4f);
                Vector3 spawnPos = new Vector3(Random.Range(-range.x / 2, range.x / 2),
                                                0,
                                                Random.Range(-range.z * 20, range.z * 20));

                if (spawnerResource > 0)
                {
                    int randomEnemy = GetEnemyToSpawn();
                    GameObject enemyGO = (Instantiate(enemyPrefabList[randomEnemy], transform.position + spawnPos, Quaternion.identity, enemyParent));
                    UseResource(enemyGO);
                }

                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }

    private int GetEnemyToSpawn()
    {
        int randomEnemy = 0;
        int maxEnemy = 0;

        if (spawnerResource >= 3 &&
            enemyPrefabList.Count >= 3)
        {
            maxEnemy = 2;
        }
        else if (spawnerResource >= 2 &&
            enemyPrefabList.Count >= 2)
        {
            maxEnemy = 1;
        }
        else if (spawnerResource >= 1 &&
            enemyPrefabList.Count >= 1)
        {
            maxEnemy = 0;
        }

        randomEnemy = Random.Range(0, maxEnemy + 1);
        return randomEnemy;
    }

    private void UseResource(GameObject enemyGO)
    {
        Enemy e = enemyGO.GetComponent<Enemy>();

        if (e.GetEnemyType() == Enemy.EnemyType.Small)
        {
            spawnerResource -= 1;
        }
        else if (e.GetEnemyType() == Enemy.EnemyType.Medium)
        {
            spawnerResource -= 2;
        }
        else if (e.GetEnemyType() == Enemy.EnemyType.Big)
        {
            spawnerResource -= 3;
        }
    }

    private void OnDrawGizmosSelected() // Turret range indicator (only inspector)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 25f);
    }
}
