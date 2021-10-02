using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    EnemyManager enemyManager;
    TimerManager timerManager;

    EnemySpawnPointBehaviour spawnPointPrefab;
    public List<EnemySpawnPointBehaviour> spawnPoints;

    float dt = 0f;
    //float spawnActivationTimer = 30f;


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
        timerManager = gameManager.GetTimerManager();
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;

        if (timerManager.GetSpawnPointActivationTimer() == 0)
        {

        }
        else if (dt >= /*spawnActivationTimer*/ timerManager.GetSpawnPointActivationTimer())
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
        return /*spawnActivationTimer*/ timerManager.GetSpawnPointActivationTimer() - dt;
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

    public void DeselectObject(GameObject obj)
    {
        if (spawnPoints != null && spawnPoints.Count > 0)
        {
            foreach (EnemySpawnPointBehaviour s in spawnPoints)
            {
                if (obj == s.gameObject)
                {
                    s.DeselectObject();
                }
            }
        }
    }

    public TimerManager GetTimerManager()
    {
        return timerManager;
    }
}
