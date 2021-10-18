using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveMngr;
    [SerializeField] private NavMeshManager navMeshMngr;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SpawnManager spawnManager;

    [Space, SerializeField] private List<GameObject> turretPool;
    [SerializeField] private List<Spawner> spawners;

    private float timeScale = 1f;

    private void Awake()
    {
        waveMngr = FindObjectOfType<WaveManager>();
        navMeshMngr = FindObjectOfType<NavMeshManager>();
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Start()
    {
        WaveIsOver();
    }

    private void Update()
    {
        Time.timeScale = timeScale;
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
        navMeshMngr.UpdateNavMeshSurface();
        waveMngr.StartNextWave(20f);

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
        else if (curWave <= 5)
        {
            return 2;
        }
        else
            return 3;
    }
}