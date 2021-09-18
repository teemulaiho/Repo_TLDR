using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    public CastleBehaviour castle;
    int experience = 0;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
    }

    private void Awake()
    {
        if (castle != null)
            castle.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExperience()
    {
        experience++;
    }

    public int GetPlayerExperience()
    {
        return experience;
    }

    public int GetPlayerStrength()
    {
        return castle.GetBulletStrength();
    }

    public void IncreaseStrength()
    {
        castle.IncreaseStrength();
        experience -= gameManager.GetStrengthUpgradeCost();
    }
}
