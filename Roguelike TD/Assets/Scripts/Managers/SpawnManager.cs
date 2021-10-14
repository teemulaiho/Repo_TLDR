using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform spawnerParent;
    [SerializeField] Spawner spawnerPrefab;
    [SerializeField] Transform baseTransform;
    [Space]
    [SerializeField] List<Spawner> spawnerList;

    private void Awake()
    {
        spawnerParent = GameObject.Find("SPAWNERPARENT").transform;

        if (spawnerPrefab == null)
            spawnerPrefab = Resources.Load<Spawner>("Prefabs/Spawner");

        spawnerList = new List<Spawner>();
    }

    private float SetSpawnPosition(float yDirection, int spawnerCount, int i)
    {
        float spawnerSpacing = Random.Range(0f, 360f) + 64f;

        if (spawnerCount % 2 != 0)
        {
            float myAngle = 0 - ((spawnerCount - 1) / 2 * spawnerSpacing) + (i * spawnerSpacing);
            yDirection += myAngle;
        }
        else if (spawnerCount % 2 == 0)
        {
            float myAngle = 0 - (((spawnerCount / 2) * spawnerSpacing) - (spawnerSpacing / 2)) + (i * spawnerSpacing);
            yDirection += myAngle;
        }

        return yDirection;
    }

    private Vector3 GetSpawnerArc(int spawnerCount, int i)
    {
        float yDirection = baseTransform.transform.eulerAngles.y;
        yDirection = SetSpawnPosition(yDirection, spawnerCount, i);

        float radius = Random.Range(50f, 100f);

        float radians = (yDirection - 90) * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float z = -Mathf.Sin(radians);

        Vector3 t = new Vector3(x, 0f, z);
        t = t * radius;

        t += baseTransform.transform.position;

        return t;
    }

    public void SpawnSpawner(int spawnerCount)
    {
        for (int i = 0; i < spawnerCount; i++)
        {
            Spawner s = Instantiate<Spawner>(spawnerPrefab);
            s.transform.parent = spawnerParent;
            s.transform.position = GetSpawnerArc(spawnerCount, i);
            s.transform.LookAt(baseTransform);

            spawnerList.Add(s);
        }
    }

    public void ResetSpawners()
    {
        foreach(Spawner s in spawnerList)
        {
            s.ClearEnemies();
            Destroy(s.gameObject);
        }

        spawnerList.Clear();
    }

    public void ActivateSpawners()
    {
        foreach (Spawner s in spawnerList)
        {
            s.SetActiveBool(true);
        }
    }
}
