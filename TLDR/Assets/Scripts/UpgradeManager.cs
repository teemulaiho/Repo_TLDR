using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    GameManager gameManager;

    int strengthLevel = 1;

    int strengthUpgradeCost = 10;
    int speedUpgraceCost = 10;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetStrengthUpgradeCost()
    {
        return strengthUpgradeCost;
    }

    public void IncreaseStrengthLevel()
    {
        strengthLevel++;
        IncreaseStrengthUpgradeCost();
    }

    private void IncreaseStrengthUpgradeCost()
    {
        strengthUpgradeCost++;
    }
}
