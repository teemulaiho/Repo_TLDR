using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] SpawnManager spawnManager;

    private float waveTimer;
    private bool waveIncoming;

    [Space]
    [SerializeField] private int waveCount = 0;

    public void StartNextWave(float waveLength)
    {
        waveTimer = waveLength;
        waveIncoming = true;

        waveCount++;
        spawnManager.SpawnSpawner(waveCount);
        spawnManager.ActivateSpawners();
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

                gameManager.WaveIsOver();
                uiManager.ResetNextWaveButton();
                spawnManager.ResetSpawners();

                waveIncoming = false;
            }
        }        
    }
}
