using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType_ExplodeOnImpact : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Turret turretScript;


    private void Awake()
    {
        if (bulletPrefab == null)
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }
    // Start is called before the first frame update
    void Start()
    {
        turretScript = GetComponent<Turret>();
    }

    private void Shoot(Transform target)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
