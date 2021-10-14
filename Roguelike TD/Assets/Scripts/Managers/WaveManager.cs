using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] TurretSelection turretSelection;

    private float waveTimer;

    private bool waveIncoming;

    public bool WaveIncomingCheck() { return waveIncoming; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        turretSelection = FindObjectOfType<TurretSelection>();
    }

    public void StartNextWave(float waveLength)
    {
        turretSelection.DropTurret();
        waveTimer = waveLength;
        waveIncoming = true;
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

                waveIncoming = false;
            }
        }
        
    }
}
