using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] Transform spawnerParent;
    [SerializeField] Spawner spawnerPrefab;
    [SerializeField] Transform baseTransform;
    [Space]
    [SerializeField] List<Spawner> spawnerList;

    [SerializeField] private LayerMask layersToAvoid = new LayerMask();

    private void Awake()
    {
        spawnerParent = GameObject.Find("SPAWNERPARENT").transform;

        if (spawnerPrefab == null)
            spawnerPrefab = Resources.Load<Spawner>("Prefabs/Spawner");

        spawnerList = new List<Spawner>();

        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    private float SetSpawnPosition(float yDirection, int spawnerCount, int i)
    {
        float spawnerSpacing = Random.Range(0f, 360f) + 64f;

        if (spawnerCount % 2 != 0)
        {
            float myAngle = 0 - ((spawnerCount - 1) / 2 * spawnerSpacing) + (spawnerSpacing); //(i * spawnerSpacing)
            yDirection += myAngle;
        }
        else if (spawnerCount % 2 == 0)
        {
            float myAngle = 0 - (((spawnerCount / 2) * spawnerSpacing) - (spawnerSpacing / 2)) + (spawnerSpacing); //(i * spawnerSpacing)
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
            s.SetSpawnerResource(waveManager.GetWaveCount() * 2 + 2);

            // Calculate new position if too close to another spawner.
            ReplaceToLegalPosition(s);
            // Switch scaling of the spawner aswell, if you want.
            s.transform.LookAt(baseTransform);

            spawnerList.Add(s);
        }
    }

    public void ClearSpawners()
    {
        foreach(Spawner s in spawnerList)
        {
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

    public void DeactivateSpawners()
    {
        foreach (Spawner s in spawnerList)
        {
            s.SetActiveBool(false);
        }
    }

    public void ReplaceToLegalPosition(Spawner s)
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(s.transform.position, 25f, layersToAvoid, QueryTriggerInteraction.Collide);

            if (hitColliders.Length <= 0)
            {
                break;
            }
            else
                s.transform.position = GetSpawnerArc(0, 0);
        }
    }
}
