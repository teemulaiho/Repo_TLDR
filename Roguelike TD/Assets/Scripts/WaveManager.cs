using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;

    private float waveTimer;

    private bool waveIncoming;

    public void StartNextWave(float waveLength)
    {
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

                waveIncoming = false;
            }
        }
        
    }
}
