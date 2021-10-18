using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private List<GameObject> enemyPrefabList;
    [SerializeField] private Transform spriteTransform;

    [Space, SerializeField] private int spawnerResource = 0;

    private bool inCombat = true;
    [Space, SerializeField] private bool active = true;

    private Vector3 range;

    public void SetSpawnerResource(int value)
    {
        spawnerResource = value;
    }

    public void SetActiveBool(bool state)
    {
        active = state;
    }

    private void Awake()
    {
        enemyParent = GameObject.Find("ENEMYPARENT").transform;
        waveManager = FindObjectOfType<WaveManager>();
        enemyPrefabList = new List<GameObject>();

        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/MediumEnemy"));
        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/BigEnemy"));
        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/SmallEnemy"));
        enemyPrefabList.Add(Resources.Load<GameObject>("Prefabs/Enemies/EnemyGroup_3Small"));
    }

    private void Start()
    {
        range = spriteTransform.localScale;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (inCombat)
        {
            while (active)
            {
                float waitTime = Random.Range(1.5f, 4f);
                Vector3 spawnPos = new Vector3(Random.Range(-range.x, range.x),
                                                0,
                                                Random.Range(-range.z * 2, range.z * 2));

                int randomEnemy = Random.Range(0, enemyPrefabList.Count);
                Instantiate(enemyPrefabList[randomEnemy], transform.position + spawnPos, Quaternion.identity, enemyParent);

                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }

    public bool CheckIfSpawnable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);

        if (hitColliders.Length <= 1)
        {
            return true;
        }
        else
            return false;
    }
}
