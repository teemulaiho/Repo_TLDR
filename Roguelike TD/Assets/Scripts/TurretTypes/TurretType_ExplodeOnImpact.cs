using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretType_ExplodeOnImpact : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootposGo;
    private Turret turretScript;


    private void Awake()
    {
        if (bulletPrefab == null)
            bulletPrefab = Resources.Load<Bullet>("Prefabs/ExplodingBullet");

        if (shootposGo == null)
            shootposGo = transform.Find("ShootPos").transform;
    
    }
    // Start is called before the first frame update
    void Start()
    {
        turretScript = GetComponent<Turret>();

        if (turretScript != null)
        {
            turretScript.ShootNow += Shoot;
        }
    }

    private void Shoot(Transform target)
    {
        SpawnBullet(target);
    }

    private void SpawnBullet(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab, shootposGo.position, shootposGo.rotation);
        
        if (bullet != null)
        {
            bullet.SetTargetPos(target.position);
            bullet.SetBulletValues(turretScript.GetBulletSpeed(), turretScript.GetBulletDamage());
        }
    }
}
