using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 70f;
    private float damage = 1f;

    private Vector3 vTarget;

    public float GetDamage() { return damage; }

    public void SetTargetPos( Vector3 vectorTarget)
    {
        vTarget = vectorTarget;
    }

    public void SetBulletValues(float bulletSpeed, float bulletDamage)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
    }

    void Update()
    {
        MoveStraight();
    }

    private void MoveStraight()
    {
        Vector3 dir = vTarget - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitDestination();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitDestination()
    {
        Destroy(gameObject);
    }
}
