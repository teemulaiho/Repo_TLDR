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
    [SerializeField] private float buffDuration = 2f;
    [SerializeField] private float speedIncrease = 0.7f;

    private void Awake()
    {
        aoeEffectArea = transform.Find("AOEEffectArea").gameObject;
        shootPosGO = transform.Find("ShootPos").gameObject.transform;

        if (aoeEffectArea != null)
            aoeEffectArea.transform.localScale = new Vector3(aoeRange * 2, 0.01f, aoeRange * 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootSpeedIncreasArea(shootPosGO.position, aoeRange);
    }

    void ShootSpeedIncreasArea(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, towerLayer);
        foreach (var hitCollider in hitColliders)
        {
            Turret t = hitCollider.gameObject.GetComponentInParent<Turret>();
            t.AddBuffIncreaseAttackSpeed(Turret.Buff.ShootSpeed, buffDuration, speedIncrease);
        }
    }

    public Vector2 GetBuffAttributes()
    {
        return new Vector2(speedIncrease, 0f);
    }
}
