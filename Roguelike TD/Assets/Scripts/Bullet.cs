using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 70f;
    [SerializeField] float damage = 1f;

    private Vector3 vTarget;

    public float GetDamage() { return damage; }

    public void Seek( Vector3 vectorTarget)
    {
        vTarget = vectorTarget;
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
