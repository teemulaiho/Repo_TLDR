using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType_AOESlowDown : MonoBehaviour
{
    
    [SerializeField] bool isActive;

    [Space]
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject aoeSlowDownArea;
    [SerializeField] private Transform shootPosGO;
    private Turret turretScript;

    [Space]
    [SerializeField] private float aoeRange = 10f;

    [Space]
    [SerializeField] private float aoeTimer = 2f;
    [SerializeField] private float dt = 0f;

    [SerializeField] private bool lerpSphere;

    private void Awake()
    {
        turretScript = GetComponent<Turret>();
        aoeSlowDownArea = transform.Find("AOESlowDownArea").gameObject;
        shootPosGO = transform.Find("ShootPos").gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (turretScript != null)
        {
            turretScript.ShootNow += Shoot;
        }

        isActive = true;
        lerpSphere = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            dt += Time.deltaTime;

            if (dt >= aoeTimer)
            {
                SlowDownArea(shootPosGO.position, aoeRange);
                dt = 0f;

                lerpSphere = true;
                //isActive = false;
            }
        }

        if (lerpSphere)
        {
            float sphereRadius = Mathf.Clamp(Mathf.PingPong(Time.time, aoeRange), aoeRange / 2, aoeRange);
            aoeSlowDownArea.transform.localScale = new Vector3(sphereRadius, 1f, sphereRadius);
      
            //if  (sphereRadius / aoeRange >= 0.9)
            //{
            //    aoeSlowDownArea.transform.localScale = Vector3.one;
            //    lerpSphere = false;
            //}
        }
    }

    private void Shoot(Transform target)
    {
        //isActive = true;
    }

    void SlowDownArea(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {   
            Enemy e = hitCollider.gameObject.GetComponentInParent<Enemy>();
            e.DebuffSlowDown(2f);
        }
    }
}
