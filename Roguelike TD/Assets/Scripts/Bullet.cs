using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 70f;
    private float damage = 1f;
    private float maxTravel = 10f;

    private Transform towerTransform;
    private Vector3 vTarget;
    private Vector3 movementDirection;

    public event Action<Transform> HitNow;

    public float GetDamage() { return damage; }

    public void SetTargetPos( Vector3 vectorTarget)
    {
        vTarget = vectorTarget;
        movementDirection = vTarget - transform.position;
    }

    public void SetBulletValues(Transform myTowerTransform,float bulletSpeed, float bulletDamage, float bulletMaxTravel)
    {
        towerTransform = myTowerTransform;
        speed = bulletSpeed;
        damage = bulletDamage;
        maxTravel = bulletMaxTravel;
    }

    void Update()
    {
        MoveStraight();
    }

    private void MoveStraight()
    {
        //Vector3 dir = vTarget - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if (dir.magnitude <= distanceThisFrame)
        //{
        //    HitDestination();
        //    return;
        //}

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.Translate(movementDirection.normalized * distanceThisFrame, Space.World);

        if (Vector3.Distance(towerTransform.position, transform.position) >= maxTravel)
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }

    public void HitDestination()
    {
        if (HitNow != null)
        {
            HitNow(transform);
        }

        Destroy(gameObject);
    }
}
