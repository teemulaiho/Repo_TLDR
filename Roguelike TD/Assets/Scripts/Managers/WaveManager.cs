using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] SpawnManager spawnManager;

    private float waveTimer;
    private bool waveIncoming;

    [Space, SerializeField] private List<Enemy> enemies = new List<Enemy>(); 

    [Space]
    [SerializeField] private int waveCount = 0;

    public bool WaveIncomingCheck() { return waveIncoming; }
    public List<Enemy> GetEmemyList() { return enemies; }
    public int GetWaveCount() { return waveCount; }
    public int GetNumberOfCracksToSpawn() 
    {
        if (waveCount <= 3)
            return 1;
        else if (waveCount <= 6)
            return 2;
        else
            return 3;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void StartNextWave(float waveLength)
    {
        waveTimer = waveLength;
        waveIncoming = true;

        waveCount++;

        uiManager.UpdateWaveCounterText();
        //spawnManager.ActivateSpawners();
    }

    private void Update()
    {
        if (waveIncoming)
        {
            if (waveTimer > 0)
            {
                waveTimer -= Time.deltaTime;
                uiManager.DisplayTime(waveTimer);
            }
            else
            {
                waveTimer = 0;
                spawnManager.DeactivateSpawners();

                if (enemies.Count <= 0)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        Destroy(enemy.gameObject);
                    }
                    enemies.Clear();

                    spawnManager.ClearSpawners();
                    uiManager.ResetNextWaveButton();
                    gameManager.WaveIsOver();

                    waveIncoming = false;
                }
            }
        }        
    }
}