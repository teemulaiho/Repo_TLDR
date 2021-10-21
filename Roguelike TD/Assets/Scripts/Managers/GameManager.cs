using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Health baseHealth;
    [SerializeField] private WaveManager waveMngr;
    [SerializeField] private NavMeshManager navMeshMngr;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SpawnManager spawnManager;

    [Space, SerializeField] private List<GameObject> turretPool;
    [SerializeField] private List<Spawner> spawners;

    private float timeScale = 1f;

    bool gameOver = false;

    public bool GetGameOver() { return gameOver; }

    private void Awake()
    {
        if (!baseHealth)
            baseHealth = GameObject.Find("Base").GetComponent<Health>(); 
        waveMngr = FindObjectOfType<WaveManager>();
        navMeshMngr = FindObjectOfType<NavMeshManager>();
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        gameOver = false;
        WaveIsOver();
    }

    private void Update()
    {
        Time.timeScale = timeScale;

        if (baseHealth.GetCurrentHealth() <= 0)
        {
            gameOver = true;
        }
    }

    public void SetTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
    }

    public void InitializeNewWave()
    {
        int spawnerCount = GetSpawnerCount();
        spawnManager.SpawnSpawner(spawnerCount);
    }

    public void StartNextWave()
    {
        uiManager.ActivateNextWaveButton(1);
        navMeshMngr.UpdateNavMeshSurface();
        waveMngr.StartNextWave(5f);

        spawnManager.ActivateSpawners();

        foreach (Spawner spawner in spawners)
        {
            spawner.SetActiveBool(true);
        }
    }

    public void WaveIsOver()
    {
        uiManager.ActivateTurretButtons(turretPool[0].GetComponent<Turret>(), 
                                        turretPool[1].GetComponent<Turret>(), 
                                        turretPool[2].GetComponent<Turret>());

        if (waveMngr.GetWaveCount() > 0)
            uiManager.ActivateNextWaveButton(-1);
        
        spawnManager.ClearSpawners();
        InitializeNewWave();
    }

    public int GetSpawnerCount()
    {
        int curWave = waveMngr.GetWaveCount();

        // Use computer counting (0, 1, 2, 3, etc.)
        // So the 3rd element is number 2.
        // Ie. Array[i];

        if (curWave <= 2)
        {
            return 1;
        }
        else if (curWave <= 6)
        {
            return 2;
        }
        else if (curWave <= 9)
        {
            return 3;
        } 
        else if (curWave <= 12)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}