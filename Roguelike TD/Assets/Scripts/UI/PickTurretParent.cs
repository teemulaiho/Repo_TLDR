using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickTurretParent : MonoBehaviour
{
    [SerializeField] private Transform turretParent;
    [SerializeField] private TurretSelection turretSelection;

    [Space, SerializeField] private TMP_Text firstOptionName;
    [SerializeField] private TMP_Text firstOptionDescription;
    [Space, SerializeField] private TMP_Text secondOptionName;
    [SerializeField] private TMP_Text secondOptionDescription;
    [Space, SerializeField] private TMP_Text thirdOptionName;
    [SerializeField] private TMP_Text thirdOptionDescription;

    private Turret firstTurret;
    private Turret secondTurret;
    private Turret thirdTurret;

    private void Start()
    {
        firstOptionName.transform.parent.GetComponent<Button>().onClick.AddListener(FirstButton);
        secondOptionName.transform.parent.GetComponent<Button>().onClick.AddListener(SecondButton);
        thirdOptionName.transform.parent.GetComponent<Button>().onClick.AddListener(ThirdButton);
    }

    public void UpdateButtons(Turret firstOption, Turret secondOption, Turret thirdOption)
    {
        firstTurret = firstOption;
        secondTurret = secondOption;
        thirdTurret = thirdOption;

        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        firstOptionName.text = $"<b>{firstTurret.GetTurretName()}</b>";
        firstOptionDescription.text = $"{firstTurret.GetTurretDescription()}";

        secondOptionName.text = $"<b>{secondTurret.GetTurretName()}</b>";
        secondOptionDescription.text = $"{secondTurret.GetTurretDescription()}";

        thirdOptionName.text = $"<b>{thirdTurret.GetTurretName()}</b>";
        thirdOptionDescription.text = $"{thirdTurret.GetTurretDescription()}";
    }

    public void FirstButton() { SpawnTurret(firstTurret); }
    public void SecondButton() { SpawnTurret(secondTurret); }
    public void ThirdButton() { SpawnTurret(thirdTurret); }

    private void SpawnTurret(Turret turretToSpawn)
    {
        Turret t = Instantiate(turretToSpawn, turretParent);
        turretSelection.SetGrabbedGO(t.transform.gameObject);

        gameObject.SetActive(false);

        // Tell gameManager what you picked so it can be removed from the pool
    }
}
