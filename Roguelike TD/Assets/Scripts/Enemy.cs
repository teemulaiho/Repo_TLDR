using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private float damage;

    private void Start()
    {
        enemyAgent.SetDestination(new Vector3(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            // Deal damage to base
            Health baseHealth = other.transform.GetComponent<Health>();

            if (baseHealth != null)
            {
                baseHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
