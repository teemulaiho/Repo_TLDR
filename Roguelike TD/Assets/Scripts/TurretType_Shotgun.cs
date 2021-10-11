using System.Collections.Generic;
using UnityEngine;

public class TurretType_Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPosGO;

    [SerializeField] int bulletAmount = 5;
    [SerializeField] float bulletSpacing = 16f;
    [SerializeField] float yDirection = 0f;

    [SerializeField] Transform newTarget;

    private Turret turretScript;

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
        for (int i = 0; i < bulletAmount; i++)
        {
            yDirection = transform.eulerAngles.y;

            if (bulletAmount % 2 != 0)
            {
                float myAngle = 0 - ((bulletAmount - 1) / 2 * bulletSpacing) + (i * bulletSpacing);
                yDirection += myAngle;
            }
            else if (bulletAmount % 2 == 0)
            {
                float myAngle = 0 - (((bulletAmount / 2) * bulletSpacing) - (bulletSpacing / 2)) + (i * bulletSpacing);
                yDirection += myAngle;
            }

            SpawnBullet(i, target);
        }
    }

    private Vector3 GetBulletArc(int i)
    {
        float radius = 10f;

        float radians = (yDirection - 90) * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float z = -Mathf.Sin(radians);

        Vector3 t = new Vector3(x, 0.1f, z);
        t = t * radius;

        t += transform.position;

        return t;
    }

    private void SpawnBullet(int i, Transform target)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, shootPosGO.position, shootPosGO.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        Vector3 arcPos = GetBulletArc(i);

        if (bullet != null)
        {
            bullet.Seek(arcPos);
        }
    }
}
