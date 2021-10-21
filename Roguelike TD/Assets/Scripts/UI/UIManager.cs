using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] WaveManager waveManager;
    [SerializeField] GameManager gameManager;

    [SerializeField] private PickTurretParent pickTurretParent;

    [Space, SerializeField] private GameObject buttonsParent;

    [Space, SerializeField] private TMP_Text waveCountdownText;

    [Space, SerializeField] private TMP_Text waveCounter;

    [Space, SerializeField] public List<Slider> progressSliders;
    [Space, SerializeField] public List<Button> towerButtons;

    [Space, SerializeField] public Button restartButton;

    [Space, SerializeField] public Toggle backgroundMusicToggle;
    [Space, SerializeField] public Toggle backgroundMusicSwitchToggle;

    [Header("Object Movement")]
    [Space, SerializeField] private Button nextWaveButton;
    [SerializeField] private bool moveNextWaveButton = false;
    [SerializeField] private int direction;
    [SerializeField] private Vector3 newPos;
    [SerializeField] private float t;

    private bool lerpSlider = false;
    private float targetLerpValueSecondButton = 0f;
    private float targetLerpValueThirdButton = 0f;

    private void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = GameObject.Find("ThreeOptionChoice");

        if (waveCounter == null)
            waveCounter = GameObject.Find("WaveCounterText").GetComponent<TMP_Text>();

        if (gameManager == null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (waveManager == null)
            waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        if (audioManager == null)
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (nextWaveButton == null)
            nextWaveButton = GameObject.Find("NextWaveButton").GetComponent<Button>();

        if (backgroundMusicToggle == null)
            backgroundMusicToggle = GameObject.Find("ToggleMusic").GetComponent<Toggle>();

        if (backgroundMusicSwitchToggle == null)
            backgroundMusicSwitchToggle = GameObject.Find("ToggleMusicSwitcher").GetComponent<Toggle>();

        if (restartButton == null)
        {
            restartButton = GameObject.Find("Restart Button").GetComponent<Button>();

            if (restartButton)
            {
                restartButton.gameObject.SetActive(false);
                restartButton.onClick.AddListener(gameManager.RestartGame);
            }
        }

        foreach (Slider s in progressSliders)
        {
            s.value = 0;
        }
    }

    private void Update()
    {
        UpdateProgressSliders();
        UpdateNewWaveButtonPosition(direction);
        ToggleUpdate();

        if (gameManager.GetGameOver())
        {
            restartButton.gameObject.SetActive(true);
        }

        //Debug.Log(Mathf.Lerp(1, 0, Time.deltaTime * 100f));
    }

    private void ToggleUpdate()
    {
        audioManager.ToggleBackgroundMusicMuted(!backgroundMusicToggle.isOn);
        audioManager.ToggleBackgroundMusicSwitch(!backgroundMusicSwitchToggle.isOn);
    }

    private void UpdateProgressSliders()
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

    private void UpdateNewWaveButtonPosition(int dir)
    {
        if (moveNextWaveButton)
        {
            float curX = nextWaveButton.transform.position.x;
            float targetX = Screen.width + (dir * nextWaveButton.GetComponent<RectTransform>().rect.width) ;
            t += 10.5f * Time.deltaTime;

            newPos.x = Mathf.MoveTowards(curX, targetX, Time.deltaTime * 700f);
            newPos.y = nextWaveButton.transform.position.y;
            newPos.z = nextWaveButton.transform.position.z;

            nextWaveButton.transform.position = newPos;

            Image mr = nextWaveButton.GetComponent<Image>();

            float alpha = 1f;
            float lerpSpeed = 10f;

            if (dir == 0)
                alpha = 1f;
            else if (dir < 0) // Moving outside of screen.
                alpha = Mathf.Lerp(1, 0, Time.deltaTime * lerpSpeed);
            else if (dir > 0) // Moving back to screen.
                alpha = Mathf.Lerp(0, 1, Time.deltaTime * lerpSpeed);
  
            if (newPos.x == Screen.width + (dir * nextWaveButton.GetComponent<RectTransform>().rect.width))
            {
                moveNextWaveButton = false;
            }

            mr.color = new Color(mr.color.r, mr.color.g, mr.color.b, alpha);


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

    /*public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        waveCountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }*/

    public void UpdateWaveCounterText()
    {
        int waveCount = waveManager.GetWaveCount();
        waveCounter.text = waveCount.ToString();
        waveCounter.text += " / 10";
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

        direction = dir;
        moveNextWaveButton = true;
    }

    private void EnableTurretButtons(bool isActive)
    {
        foreach (Button b in towerButtons)
        {
            b.gameObject.SetActive(isActive);
        }
    }
}