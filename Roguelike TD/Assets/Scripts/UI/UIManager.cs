using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] GameManager gameManager;

    [SerializeField] private PickTurretParent pickTurretParent;

    [Space, SerializeField] private GameObject buttonsParent;

    [Space, SerializeField] private TMP_Text waveCountdownText;

    [Space, SerializeField] public List<Button> towerButtons;

    [Space, SerializeField] public Button restartButton;
    [SerializeField] private Button nextWaveButton;

    private void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");

        if (gameManager == null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        if (nextWaveButton == null)
            nextWaveButton = GameObject.Find("NextWaveButton").GetComponent<Button>();

        if (restartButton == null)
        {
            restartButton = GameObject.Find("Restart Button").GetComponent<Button>();

            if (restartButton)
            {
                restartButton.gameObject.SetActive(false);
                restartButton.onClick.AddListener(gameManager.RestartGame);
            }
        }
    }

    public void ActivateTurretButtons(Turret firstOption, Turret secondOption, Turret thirdOption)
    {
        pickTurretParent.UpdateButtons(firstOption, secondOption, thirdOption);

        buttonsParent.SetActive(true);
    }

    public void ResetNextWaveButton()
    {
        waveCountdownText.transform.parent.GetComponent<Button>().interactable = true;
        waveCountdownText.text = "START WAVE";
    }

    public void ActivateNextWaveButton(int dir)
    {
        if (dir < 0)
        {
            nextWaveButton.interactable = true;
            EnableTurretButtons(true);
        }
        else
        {
            nextWaveButton.interactable = false;
            EnableTurretButtons(false);
        }
    }

    private void EnableTurretButtons(bool isActive)
    {
        foreach (Button b in towerButtons)
        {
            b.gameObject.SetActive(isActive);
        }
    }

    /*public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        waveCountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }*/
}