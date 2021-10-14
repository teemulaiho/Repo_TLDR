using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType_AOESlowDown : MonoBehaviour
{
    [Space]
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject aoeSlowDownArea;
    [SerializeField] private Transform shootPosGO;

    [Space]
    [SerializeField] private float aoeRange = 10f;

    [Space]
    [SerializeField] private float debuffTimer = 2f;
    [SerializeField] private float slowDownMultiplier = 0.7f;

    private void Awake()
    {
        aoeSlowDownArea = transform.Find("AOESlowDownArea").gameObject;
        shootPosGO = transform.Find("ShootPos").gameObject.transform;

        if (aoeSlowDownArea != null)
            aoeSlowDownArea.transform.localScale = new Vector3(aoeRange * 2, 0.01f, aoeRange * 2);
    }

    // Update is called once per frame
    void Update()
    {
        SlowDownArea(shootPosGO.position, aoeRange);
    }

    void SlowDownArea(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {   
            Enemy e = hitCollider.gameObject.GetComponentInParent<Enemy>();
            e.DebuffSlowDown(debuffTimer, slowDownMultiplier);
        }
    }
}
