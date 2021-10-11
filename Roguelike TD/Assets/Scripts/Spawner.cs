using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spriteTransform;

    private bool inCombat = true;
    [Space, SerializeField] private bool active = true;

    private Vector3 range;

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
                Instantiate(enemyPrefab, transform.position + spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }
}
