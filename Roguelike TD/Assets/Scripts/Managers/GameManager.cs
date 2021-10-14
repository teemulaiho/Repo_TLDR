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
        spawnManager.SpawnSpawner(1);
    }

    public void StartNextWave()
    {
        navMeshMngr.UpdateNavMeshSurface();
        waveMngr.StartNextWave(20f);

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

        spawnManager.ResetSpawners();
        spawnManager.SpawnSpawner(1);
    }
}