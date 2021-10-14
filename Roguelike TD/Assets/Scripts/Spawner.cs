using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform enemyParent;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spriteTransform;

    private bool inCombat = true;
    [Space, SerializeField] private bool active = true;

    private Vector3 range;

    [Space]
    [SerializeField] private List<GameObject> enemyList;

    public void SetActiveBool(bool state)
    {
        active = state;
    }

    private void Awake()
    {
        enemyParent = GameObject.Find("ENEMYPARENT").transform;
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
                                                Random.Range(-range.y, range.y),
                                                Random.Range(-range.z * 4, range.z * 4));
                enemyList.Add(Instantiate(enemyPrefab, transform.position + spawnPos, Quaternion.identity, enemyParent));

                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }

    public void ClearEnemies()
    {
        foreach(GameObject go in enemyList)
        {
            Destroy(go);
        }

        enemyList.Clear();
    }
}
