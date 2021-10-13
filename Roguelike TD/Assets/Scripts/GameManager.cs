using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveMngr;
    [SerializeField] private NavMeshManager navMeshMngr;
    [SerializeField] private UIManager uiManager;

    [Space, SerializeField] private List<GameObject> turretPool;
    [SerializeField] private List<Spawner> spawners;

    private void Start()
    {
        WaveIsOver();
    }

    public void StartNextWave()
    {
        navMeshMngr.UpdateNavMeshSurface();
        waveMngr.StartNextWave(30f);

        foreach (Spawner spawner in spawners)
        {
            spawner.SetActiveBool(true);
        }
    }

    public void WaveIsOver()
    {
        uiManager.ActivateTurretButtons(turretPool[0], turretPool[1], turretPool[2]);

        RemoveOldSpawners();
        SpawnNewSpawners();
    }

    private void RemoveOldSpawners()
    {
        foreach (Spawner spawner in spawners)
        {
            Destroy(spawner.gameObject);
            spawners.Remove(spawner);
        }
    }

    private void SpawnNewSpawners() 
    { 
        // Spawn in new spawner from prefab (circle thing here?)
        // Add spawners to list
    }
}