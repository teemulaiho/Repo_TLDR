using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    GameManager gameManager;

    public List<float> timers;

    float spawnPointTimer = 8f;
    float spawnPointDeactivationTimer = 30f;
    float spawnPointActivationTimer = 30f;
    float enemySpawnRateTimer = 8f;
    float enemyHealth = 0f;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.GetTimers();
    }

    // Update is called once per frame
    void Update()
    {
        timers.Clear();

        timers.AddRange(gameManager.GetTimers());
    }

    public void AddTimer(ref float timer)
    {
        timers.Add(timer);
    }

    public float GetSpawnPointTimer()
    {
        return spawnPointTimer;
    }

    public float GetSpawnPointDeactivationTimer()
    {
        return spawnPointDeactivationTimer;
    }

    public float GetSpawnPointActivationTimer()
    {
        return spawnPointActivationTimer;
    }

    public float GetEnemySpawnRateTimer()
    {
        return enemySpawnRateTimer;
    }

    public void SetSpawnPointActivationTimer(float value)
    {
        spawnPointActivationTimer = value; 
    }

    public void SetEnemySpawnRateTimer(float value)
    {
        enemySpawnRateTimer = value;
    }
}
