using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    EnemyManager enemyManager;

    EnemySpawnPointBehaviour spawnPointPrefab;
    public List<EnemySpawnPointBehaviour> spawnPoints;

    float dt = 0f;
    float spawnActivationTimer = 10f;


    public void Initialize(GameManager gm, EnemyManager em)
    {
        gameManager = gm;
        enemyManager = em;

        CreateSpawnPoint();
    }

    private void Awake()
    {
        spawnPointPrefab = Resources.Load<EnemySpawnPointBehaviour>("Prefabs/EnemySpawnPoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;

        if (dt >= spawnActivationTimer)
        {
            dt = 0f;
            CreateSpawnPoint();
        }
    }

    private void CreateSpawnPoint()
    {
        EnemySpawnPointBehaviour spawnPoint = Instantiate(spawnPointPrefab, this.transform);
        spawnPoint.name = "EnemySpawnPoint";
        spawnPoint.transform.position = new Vector3(Random.Range(0f, 50f), 1f, Random.Range(0f, 50f));
        spawnPoint.Initialize(this, enemyManager);
        spawnPoints.Add(spawnPoint);
    }

    public float GetSpawnManagerTimer()
    {
        return dt;
    }

    public List<float> GetSpawnPointTimers()
    {
        List<float> t = new List<float>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            t.Add(spawnPoints[i].GetTimer());
        }

        return t;
    }

    public float GetTimeLeftUntilNextEnemySpawnPoint()
    {
        return spawnActivationTimer - dt;
    }

    public void SelectObject(GameObject obj, int mouseButton)
    {
        if (spawnPoints != null && spawnPoints.Count > 0)
        {
            foreach (EnemySpawnPointBehaviour s in spawnPoints)
            {
                if (obj == s.gameObject)
                {
                    s.SelectObject(mouseButton);
                }
            }
        }
    }
}
