using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PickTurretParent pickTurretParent;

    [Space, SerializeField] private GameObject buttonsParent;

    [Space, SerializeField] private TMP_Text waveCountdownText;

    private void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");
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
}