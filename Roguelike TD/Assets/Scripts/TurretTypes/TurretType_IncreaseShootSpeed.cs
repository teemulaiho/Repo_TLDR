using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType_IncreaseShootSpeed : MonoBehaviour
{
    [Space]
    [SerializeField] LayerMask towerLayer;
    [SerializeField] GameObject aoeEffectArea;
    [SerializeField] private Transform shootPosGO;

    [Space]
    [SerializeField] private float aoeRange = 10f;

    [Space]
    [SerializeField] private float debuffTimer = 2f;
    [SerializeField] private float slowDownMultiplier = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
