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

    [Space, SerializeField] public List<Slider> progressSliders;
    [Space, SerializeField] public List<Button> towerButtons;

    private bool lerpSlider = false;
    private float targetLerpValueSecondButton = 0f;
    private float targetLerpValueThirdButton = 0f;

    private void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");

        if (waveCounter == null)
            waveCounter = GameObject.Find("WaveCounterText").GetComponent<TMP_Text>();

        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        foreach (Slider s in progressSliders)
        {
            s.value = 0;
        }
    }

    private void Update()
    {
        int i = 0;
        foreach (Slider s in progressSliders)
        {
            if (i == 0)
            {
                if (s.value <= targetLerpValueSecondButton - 0.01f)
                {
                    s.value = Mathf.MoveTowards(s.value, targetLerpValueSecondButton, Time.deltaTime * 0.05f);
                }
                else if (s.value >= 0.95f && !towerButtons[1].interactable)
                {
                    towerButtons[1].interactable = true;
                }
            }
            else if (i == 1)
            {
                if (s.value <= targetLerpValueThirdButton - 0.01f)
                {
                    s.value = Mathf.MoveTowards(s.value, targetLerpValueThirdButton, Time.deltaTime * 0.05f);
                }
                else if (s.value > 0.95f && !towerButtons[2].interactable)
                {
                    towerButtons[2].interactable = true;
                }
            }

            i++;
        }
    }

    public void ActivateTurretButtons(Turret firstOption, Turret secondOption, Turret thirdOption)
    {
        pickTurretParent.UpdateButtons(firstOption, secondOption, thirdOption);

        Button secondButton = pickTurretParent.transform.Find("SpawnTowerOption2").GetComponent<Button>();
        Button thirdButton = pickTurretParent.transform.Find("SpawnTowerOption3").GetComponent<Button>();

        if (waveManager.GetWaveCount() == 0)
        {
            targetLerpValueSecondButton = 0f;
            targetLerpValueThirdButton = 0f;
        }
        else if (waveManager.GetWaveCount() == 1)
        {
            targetLerpValueSecondButton = 0.33f;
            targetLerpValueThirdButton = 1f / 6f;
        }
        else if (waveManager.GetWaveCount() == 2)
        {
            targetLerpValueSecondButton = 0.66f;
            targetLerpValueThirdButton = (1f / 6f) * 2f;
        }
        else if ( waveManager.GetWaveCount() == 3)
        {
            targetLerpValueSecondButton = 1f;
            targetLerpValueThirdButton = (1f / 6f) * 3f;
        }
        else if (waveManager.GetWaveCount() == 4)
        {
            targetLerpValueThirdButton = (1f / 6f) * 4f;
        }
        else if (waveManager.GetWaveCount() == 5)
        {
            targetLerpValueThirdButton = (1f / 6f) * 5f;
        }
        else if (waveManager.GetWaveCount() == 6)
        {
            targetLerpValueThirdButton = (1f / 6f) * 6f;
        }

        //foreach(Slider s in progressSliders)
        //{
        //    float newVal = waveManager.GetWaveCount() / 3f;

        //    Debug.Log(newVal);
        //    s.value = newVal;
        //}

        // Disable Shotgun and ExplodeOnImpat Towers initally.
        if (waveManager.GetWaveCount() < 3)
        {
            secondButton.interactable = false;
            thirdButton.interactable = false; 
        }
        else if (waveManager.GetWaveCount() < 6)
        {
            //secondButton.interactable = true;
        }
        else if (waveManager.GetWaveCount() >= 6)
        {
            //thirdButton.interactable = true;
        }

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