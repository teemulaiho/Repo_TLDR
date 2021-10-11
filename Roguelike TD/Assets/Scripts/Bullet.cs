using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Transform targetStartPos;

    private Vector3 vTarget;

    [SerializeField] float speed = 70f;
    [SerializeField] float damage = 1f;

    public void Seek(Transform newTarget)
    {
        target = newTarget;
        targetStartPos = target;
    }

    public void Seek(Vector3 newTarget)
    {
        //target.position = newTarget;
        vTarget = newTarget;
    }

    void Update()
    {
        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        Vector3 dir = vTarget - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        Debug.Log("Hit Target");

        //Health targetHealth = target.GetComponent<Health>();

        //if (targetHealth)
        //{
        //    targetHealth.TakeDamage(damage);
        //}

        Destroy(gameObject);
    }
}
