using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;

    [SerializeField] private PickTurretParent pickTurretParent;

    [Space, SerializeField] private GameObject buttonsParent;

    [Space, SerializeField] private TMP_Text waveCountdownText;

    [Space, SerializeField] private TMP_Text waveCounter;

    private void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");

        if (waveCounter == null)
            waveCounter = GameObject.Find("WaveCounterText").GetComponent<TMP_Text>();

        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    private void Update()
    {

    }

    public void ActivateTurretButtons(Turret firstOption, Turret secondOption, Turret thirdOption)
    {
        pickTurretParent.UpdateButtons(firstOption, secondOption, thirdOption);

        buttonsParent.SetActive(true);
    }

    public void ResetNextWaveButton()
    {
        waveCountdownText.text = "NEXT WAVE";
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        waveCountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateWaveCounterText()
    {
        int waveCount = waveManager.GetWaveCount();
        waveCounter.text = waveCount.ToString();
        waveCounter.text += " / 10";

        //StartCoroutine(AnimateText(waveCounter));
    }

    private IEnumerator AnimateText(TMP_Text text)
    {
        bool loop = true;
        bool loopUp = true;

        float countDownTimer = 5f;

        while (countDownTimer > 1f)
        {
            while (loop)
            {
                if (loopUp)
                {
                    text.fontSize = Mathf.Lerp(text.fontSize, 48f, Time.deltaTime * 4f);
                    if (waveCounter.fontSize >= 45f)
                        loopUp = false;
                }
                else
                {
                    text.fontSize = Mathf.Lerp(text.fontSize, 36f, Time.deltaTime * 4f);
                    if (waveCounter.fontSize <= 38f)
                    {
                        text.fontSize = 36f;
                        loop = false;
                    }
                }

                Debug.Log(text.fontSize);

                countDownTimer -= Time.deltaTime;
            }

            yield return null;
        }
    }
}